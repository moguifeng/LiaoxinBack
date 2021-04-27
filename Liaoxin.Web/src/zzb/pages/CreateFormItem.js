import React, { Component } from 'react';
import { Form, Input, Tree, Select, InputNumber, DatePicker } from 'antd';
import moment from 'moment';
import { Editor } from 'react-draft-wysiwyg';
import { EditorState, convertToRaw, ContentState } from 'draft-js';
import draftToHtml from 'draftjs-to-html';
import htmlToDraft from 'html-to-draftjs';
import '../../../node_modules/react-draft-wysiwyg/dist/react-draft-wysiwyg.css';
import ImageItem from './ImageItem';

const FormItem = Form.Item;
const { TreeNode } = Tree;
const { Option } = Select;

class CreateFormItem extends Component {
  state = {
    checkedKeys: [],
    editorState: [],
  };

  componentDidMount() {
    const { field } = this.props;
    const { checkedKeys } = this.state;
    if (field.value && !checkedKeys[field.id]) {
      checkedKeys[field.id] = field.value.split(',');
      this.setState({ checkedKeys });
    }
  }

  onEditorStateChange = (id, state) => {
    const { editorState } = this.state;
    editorState[id] = state;
    this.setState({ editorState });
    this.handleChange(draftToHtml(convertToRaw(state.getCurrentContent())), id);
  };

  createInput = (field, option) => {
    const { checkedKeys, editorState } = this.state;
    const { form } = this.props;
    let pla = field.title;
    if (field.placeholder) {
      pla = field.placeholder;
    }
    if (field.type === 'PasswordFieldAttribute') {
      return form.getFieldDecorator(field.id, option)(
        <Input type="password" placeholder={pla} disabled={!!field.isReadOnly} />
      );
    }
    if (field.type === 'HiddenTextFieldAttribute') {
      return form.getFieldDecorator(field.id, option)(
        <Input type="hidden" placeholder={pla} disabled={!!field.isReadOnly} />
      );
    }
    if (field.type === 'TreeFieldAttribute') {
      return (
        <div>
          {form.getFieldDecorator(field.id, { ...option, initialValue: field.value.split(',') })(
            <Input type="hidden" placeholder={pla} disabled={!!field.isReadOnly} />
          )}
          <Tree
            checkable
            defaultExpandAll
            onCheck={k => {
              this.onCheck(k, field.id);
            }}
            checkedKeys={checkedKeys[field.id]}
          >
            {this.renderTreeNodes(this.createTree(field.source))}
          </Tree>
        </div>
      );
    }
    if (field.type === 'DropListFieldAttribute') {
      return (
        <div>
          {form.getFieldDecorator(field.id, { ...option, initialValue: field.value })(
            <Input type="hidden" placeholder={pla} disabled={!!field.isReadOnly} />
          )}
          <Select
            defaultValue={field.value}
            style={{ width: '100%' }}
            onChange={v => this.handleChange(v, field.id)}
          >
            {field.source.map(t => (
              <Option key={t.key} value={t.key}>
                {t.value}
              </Option>
            ))}
          </Select>
        </div>
      );
    }

    if (field.type === 'NumberFieldAttribute') {
      return (
        <div>
          {form.getFieldDecorator(field.id, { ...option, initialValue: field.value })(
            <Input type="hidden" placeholder={pla} disabled={!!field.isReadOnly} />
          )}
          <InputNumber
            style={{ width: 295 }}
            placeholder={pla}
            disabled={!!field.isReadOnly}
            defaultValue={field.value}
            onChange={k => this.handleChange(k, field.id)}
          />
        </div>
      );
    }

    if (field.type === 'DateTimeFieldAttribute') {
      return (
        <div>
          {form.getFieldDecorator(field.id, { ...option, initialValue: field.value })(
            <Input type="hidden" placeholder={pla} disabled={!!field.isReadOnly} />
          )}
          <DatePicker
            style={{ width: 295 }}
            allowClear={false}
            onChange={(_, s) => {
              this.handleChange(s, field.id);
            }}
            defaultValue={field.value && moment(field.value, 'YYYY-MM-DD')}
          />
        </div>
      );
    }

    if (field.type === 'EditorFieldAttribute') {
      if (!editorState[field.id]) {
        if (field.value) {
          const contentBlock = htmlToDraft(field.value);
          if (contentBlock) {
            const contentState = ContentState.createFromBlockArray(contentBlock.contentBlocks);
            editorState[field.id] = EditorState.createWithContent(contentState);
            this.setState({ editorState });
          }
        } else {
          editorState[field.id] = EditorState.createEmpty();
          this.setState({ editorState });
        }
      }
      return (
        <div>
          {form.getFieldDecorator(field.id, { ...option, initialValue: field.value })(
            <Input type="hidden" placeholder={pla} disabled={!!field.isReadOnly} />
          )}
          <FormItem labelCol={{ span: 5 }} label={field.title} />
          <Editor
            localization={{ locale: 'zh' }}
            editorState={editorState[field.id]}
            wrapperClassName="wysiwyg-wrapper"
            editorClassName="demo-editor"
            onEditorStateChange={s => {
              this.onEditorStateChange(field.id, s);
            }}
          />
        </div>
      );
    }
    if (field.type === 'ImageFieldAttribute') {
      return (
        <ImageItem field={field} form={form} option={option} handleChange={this.handleChange} />
      );
    }
    return form.getFieldDecorator(field.id, option)(
      <Input placeholder={pla} disabled={!!field.isReadOnly} />
    );
  };

  handleChange = (k, id) => {
    const { form } = this.props;
    form.setFieldsValue({ [id]: k });
  };

  onCheck = (k, id) => {
    const { checkedKeys } = this.state;
    const { form } = this.props;
    checkedKeys[id] = k;
    this.setState({ checkedKeys });
    form.setFieldsValue({ [id]: k });
  };

  renderTreeNodes = data =>
    data &&
    data.map(item => {
      if (item.children) {
        return (
          <TreeNode title={item.title} key={item.key} dataRef={item}>
            {this.renderTreeNodes(item.children)}
          </TreeNode>
        );
      }
      return <TreeNode {...item} />;
    });

  createTree = source => {
    if (source) {
      return source.map(s => {
        const children = this.createTree(s.treesModels);
        return {
          key: s.value,
          title: s.title,
          children,
        };
      });
    }
    return null;
  };

  render() {
    const { field } = this.props;
    const option = {
      rules: [{ required: !!field.isRequired, message: `请输入${field.title}` }],
    };
    if (field.value) {
      option.initialValue = field.value;
    }
    if (field.type === 'HiddenTextFieldAttribute' || field.type === 'EditorFieldAttribute') {
      return this.createInput(field, option);
    }

    return (
      <FormItem labelCol={{ span: 5 }} wrapperCol={{ span: 15 }} label={field.title}>
        {/* {form.getFieldDecorator(field.id, option)(this.createInput(field))} */}
        {this.createInput(field, option)}
      </FormItem>
    );
  }
}

export default CreateFormItem;
