import React, { PureComponent } from 'react';
import { connect } from 'dva';
import {
  Row,
  Col,
  Card,
  Form,
  Input,
  Select,
  Icon,
  Button,
  Dropdown,
  Menu,
  DatePicker,
  Modal,
  message,
  Steps,
  Radio,
} from 'antd';
import zzbAction from '../action';
import StandardTable from '../../components/StandardTable';
import PageHeaderWrapper from '../../components/PageHeaderWrapper';
import CreateForm from './CreateForm';
import styles from '../../pages/List/TableList.less';
import CreateFormItem from './CreateFormItem';

const FormItem = Form.Item;
const { Step } = Steps;
const { TextArea } = Input;
const { Option } = Select;
const RadioGroup = Radio.Group;

@Form.create()
class UpdateForm extends PureComponent {
  static defaultProps = {
    handleUpdate: () => {},
    handleUpdateModalVisible: () => {},
    values: {},
  };

  constructor(props) {
    super(props);

    this.state = {
      formVals: {
        name: props.values.name,
        desc: props.values.desc,
        key: props.values.key,
        target: '0',
        template: '0',
        type: '1',
        time: '',
        frequency: 'month',
      },
      currentStep: 0,
    };

    this.formLayout = {
      labelCol: { span: 7 },
      wrapperCol: { span: 13 },
    };
  }

  handleNext = currentStep => {
    const { form, handleUpdate } = this.props;
    const { formVals: oldValue } = this.state;
    form.validateFields((err, fieldsValue) => {
      if (err) return;
      const formVals = { ...oldValue, ...fieldsValue };
      this.setState(
        {
          formVals,
        },
        () => {
          if (currentStep < 2) {
            this.forward();
          } else {
            handleUpdate(formVals);
          }
        }
      );
    });
  };

  backward = () => {
    const { currentStep } = this.state;
    this.setState({
      currentStep: currentStep - 1,
    });
  };

  forward = () => {
    const { currentStep } = this.state;
    this.setState({
      currentStep: currentStep + 1,
    });
  };

  renderContent = (currentStep, formVals) => {
    const { form } = this.props;
    if (currentStep === 1) {
      return [
        <FormItem key="target" {...this.formLayout} label="监控对象">
          {form.getFieldDecorator('target', {
            initialValue: formVals.target,
          })(
            <Select style={{ width: '100%' }}>
              <Option value="0">表一</Option>
              <Option value="1">表二</Option>
            </Select>
          )}
        </FormItem>,
        <FormItem key="template" {...this.formLayout} label="规则模板">
          {form.getFieldDecorator('template', {
            initialValue: formVals.template,
          })(
            <Select style={{ width: '100%' }}>
              <Option value="0">规则模板一</Option>
              <Option value="1">规则模板二</Option>
            </Select>
          )}
        </FormItem>,
        <FormItem key="type" {...this.formLayout} label="规则类型">
          {form.getFieldDecorator('type', {
            initialValue: formVals.type,
          })(
            <RadioGroup>
              <Radio value="0">强</Radio>
              <Radio value="1">弱</Radio>
            </RadioGroup>
          )}
        </FormItem>,
      ];
    }
    if (currentStep === 2) {
      return [
        <FormItem key="time" {...this.formLayout} label="开始时间">
          {form.getFieldDecorator('time', {
            rules: [{ required: true, message: '请选择开始时间！' }],
          })(
            <DatePicker
              style={{ width: '100%' }}
              showTime
              format="YYYY-MM-DD HH:mm:ss"
              placeholder="选择开始时间"
            />
          )}
        </FormItem>,
        <FormItem key="frequency" {...this.formLayout} label="调度周期">
          {form.getFieldDecorator('frequency', {
            initialValue: formVals.frequency,
          })(
            <Select style={{ width: '100%' }}>
              <Option value="month">月</Option>
              <Option value="week">周</Option>
            </Select>
          )}
        </FormItem>,
      ];
    }
    return [
      <FormItem key="name" {...this.formLayout} label="规则名称">
        {form.getFieldDecorator('name', {
          rules: [{ required: true, message: '请输入规则名称！' }],
          initialValue: formVals.name,
        })(<Input placeholder="请输入" />)}
      </FormItem>,
      <FormItem key="desc" {...this.formLayout} label="规则描述">
        {form.getFieldDecorator('desc', {
          rules: [{ required: true, message: '请输入至少五个字符的规则描述！', min: 5 }],
          initialValue: formVals.desc,
        })(<TextArea rows={4} placeholder="请输入至少五个字符" />)}
      </FormItem>,
    ];
  };

  renderFooter = currentStep => {
    const { handleUpdateModalVisible, values } = this.props;
    if (currentStep === 1) {
      return [
        <Button key="back" style={{ float: 'left' }} onClick={this.backward}>
          上一步
        </Button>,
        <Button key="cancel" onClick={() => handleUpdateModalVisible(false, values)}>
          取消
        </Button>,
        <Button key="forward" type="primary" onClick={() => this.handleNext(currentStep)}>
          下一步
        </Button>,
      ];
    }
    if (currentStep === 2) {
      return [
        <Button key="back" style={{ float: 'left' }} onClick={this.backward}>
          上一步
        </Button>,
        <Button key="cancel" onClick={() => handleUpdateModalVisible(false, values)}>
          取消
        </Button>,
        <Button key="submit" type="primary" onClick={() => this.handleNext(currentStep)}>
          完成
        </Button>,
      ];
    }
    return [
      <Button key="cancel" onClick={() => handleUpdateModalVisible(false, values)}>
        取消
      </Button>,
      <Button key="forward" type="primary" onClick={() => this.handleNext(currentStep)}>
        下一步
      </Button>,
    ];
  };

  render() {
    const { updateModalVisible, handleUpdateModalVisible, values } = this.props;
    const { currentStep, formVals } = this.state;

    return (
      <Modal
        width={640}
        bodyStyle={{ padding: '32px 40px 48px' }}
        destroyOnClose
        title="规则配置"
        visible={updateModalVisible}
        footer={this.renderFooter(currentStep)}
        onCancel={() => handleUpdateModalVisible(false, values)}
        afterClose={() => handleUpdateModalVisible()}
      >
        <Steps style={{ marginBottom: 28 }} size="small" current={currentStep}>
          <Step title="基本信息" />
          <Step title="配置规则属性" />
          <Step title="设定调度周期" />
        </Steps>
        {this.renderContent(currentStep, formVals)}
      </Modal>
    );
  }
}

/* eslint react/no-multi-comp:0 */
@connect(({ rule, loading, tableInfomation, rows, modal }) => ({
  modal,
  rows,
  tableInfomation,
  rule,
  loading: loading.models.rule,
}))
@Form.create()
class ZzbTableListDetail extends PureComponent {
  state = {
    modalVisible: false,
    updateModalVisible: false,
    expandForm: false,
    selectedRows: [],
    formValues: {},
    stepFormValues: {},
    modalId: '',
    current: 1,
    pageSize: 10,
  };

  componentDidMount() {
    const { dispatch, id, form } = this.props;
    const callback = this.handleSearch;
    dispatch({
      type: 'tableInfomation/getInfomation',
      navId: id,
      handelButton: this.handelButton,
      callBack: () => {
        callback();
      },
      form,
      query: () => {
        form.validateFields((err, fieldsValue) => {
          if (err) return;

          const values = {
            ...fieldsValue,
            updatedAt: fieldsValue.updatedAt && fieldsValue.updatedAt.valueOf(),
          };
          this.setState({
            formValues: values,
          });
          dispatch({
            type: 'rows/getRowsData',
            query: values,
            navId: id,
          });
        });
      },
    });
    // dispatch({
    //   type: 'rows/getRowsData',
    //   navId: id,
    // });
  }

  handleStandardTableChange = pagination => {
    const { dispatch, id } = this.props;
    const { formValues } = this.state;

    dispatch({
      type: 'rows/getRowsData',
      index: pagination.current,
      size: pagination.pageSize,
      navId: id,
      query: formValues,
    });

    this.setState({ current: pagination.current, pageSize: pagination.pageSize });
  };

  handleFormReset = () => {
    const { form, dispatch, id } = this.props;
    form.resetFields();
    this.setState({
      formValues: {},
    });
    dispatch({
      type: 'rows/getRowsData',
      navId: id,
    });
  };

  toggleForm = () => {
    const { expandForm } = this.state;
    this.setState({
      expandForm: !expandForm,
    });
  };

  handleMenuClick = e => {
    const { dispatch } = this.props;
    const { selectedRows } = this.state;

    if (selectedRows.length === 0) return;
    switch (e.key) {
      case 'remove':
        dispatch({
          type: 'rule/remove',
          payload: {
            key: selectedRows.map(row => row.key),
          },
          callback: () => {
            this.setState({
              selectedRows: [],
            });
          },
        });
        break;
      default:
        break;
    }
  };

  handleSelectRows = rows => {
    this.setState({
      selectedRows: rows,
    });
  };

  handleSearch = e => {
    if (e) e.preventDefault();
    const { dispatch, form, id } = this.props;
    const { current, pageSize } = this.state;

    form.validateFields((err, fieldsValue) => {
      if (err) return;

      const values = {
        ...fieldsValue,
        updatedAt: fieldsValue.updatedAt && fieldsValue.updatedAt.valueOf(),
      };
      this.setState({
        formValues: values,
      });
      dispatch({
        type: 'rows/getRowsData',
        query: values,
        navId: id,
        index: current,
        size: pageSize,
      });
    });
  };

  handelButton = (button, data) => {
    const { dispatch, id } = this.props;
    const { handleSearch } = this;
    if (button.buttonType === 'ConfirmActionButton') {
      Modal.confirm({
        title: button.confirmMessage,
        onOk() {
          dispatch({
            type: 'rows/handleRowAction',
            navId: id,
            data,
            buttonId: button.id,
            callBack: handleSearch,
          });
        },
        okText: '确定',
        cancelText: '取消',
        maskClosable: true,
      });
    }
    console.log(button)
    if (button.buttonType === 'ModalButton') {
      this.handleModalVisible(true, button.modalId, data);
    }
  };

  handleModalVisible = (flag, modalId, data) => {
    const { dispatch } = this.props;
    const hasFlag = !!flag;
    this.setState({
      modalVisible: hasFlag,
      modalId,
    });
    if (hasFlag) {
      dispatch({ type: 'modal/getViewModalInfo', modalId, data });
    } else {
      this.handleSearch();
    }
  };

  handleUpdateModalVisible = (flag, record) => {
    this.setState({
      updateModalVisible: !!flag,
      stepFormValues: record || {},
    });
  };

  handleAdd = fields => {
    const { dispatch } = this.props;
    dispatch({
      type: 'rule/add',
      payload: {
        desc: fields.desc,
      },
    });

    message.success('添加成功');
    this.handleModalVisible();
  };

  handleUpdate = fields => {
    const { dispatch } = this.props;
    dispatch({
      type: 'rule/update',
      payload: {
        name: fields.name,
        desc: fields.desc,
        key: fields.key,
      },
    });

    message.success('配置成功');
    this.handleUpdateModalVisible();
  };

  createFormIten(field) {
    const { form } = this.props;
    return (
      // <FormItem label={field.name}>
      //   {getFieldDecorator(field.id)(<Input placeholder={field.name} />)}
      // </FormItem>
      <CreateFormItem form={form} field={field} />
    );
  }

  viewButton() {
    const { tableInfomation, form } = this.props;
    return (
      tableInfomation.viewButton &&
      tableInfomation.viewButton.map(b => (
        <Button
          key={b.id}
          icon={b.icon}
          type={b.type}
          onClick={() => {
            if (b.modalId) {
              this.handleModalVisible(true, b.modalId);
            }
            if (b.actionType) {
              zzbAction(b.actionType, {
                form,
                query: () => {
                  this.handleSearch();
                },
              });
            }
          }}
          style={{ marginLeft: 8 }}
        >
          {b.name}
        </Button>
      ))
    );
  }

  renderSimpleForm() {
    const {
      tableInfomation: { querys },
    } = this.props;
    if (querys) {
      const qs = querys.filter(t => t.type !== 'HiddenTextFieldAttribute');
      const hiddens = querys.filter(t => t.type === 'HiddenTextFieldAttribute');
      if (qs && qs.length > 0) {
        let first = <Col md={8} sm={24} />;
        if (qs[0]) {
          first = (
            <Col md={8} sm={24}>
              {this.createFormIten(qs[0])}
            </Col>
          );
        }
        let second = <Col md={8} sm={24} />;
        if (qs[1]) {
          second = (
            <Col md={8} sm={24}>
              {this.createFormIten(qs[1])}
            </Col>
          );
        }
        return (
          <Form onSubmit={this.handleSearch} layout="inline">
            {hiddens && hiddens.map(r => <div key={r.id}>{this.createFormIten(r)}</div>)}
            <Row gutter={{ md: 8, lg: 24, xl: 48 }}>
              {first}
              {second}
              <Col md={8} sm={24}>
                <span className={styles.submitButtons}>
                  <Button type="primary" htmlType="submit">
                    查询
                  </Button>
                  {this.viewButton()}
                  {qs.length > 2 && (
                    <a style={{ marginLeft: 8 }} onClick={this.toggleForm}>
                      展开 <Icon type="down" />
                    </a>
                  )}
                </span>
              </Col>
            </Row>
          </Form>
        );
      }
    }
    return (
      <Row gutter={{ md: 8, lg: 24, xl: 48 }}>
        <Col md={8} sm={24} />
        <Col md={8} sm={24} />
        <Col md={8} sm={24}>
          <span className={styles.submitButtons}>
            <Button
              type="primary"
              htmlType="submit"
              onClick={() => {
                this.handleSearch();
              }}
            >
              查询
            </Button>
            {this.viewButton()}
          </span>
        </Col>
      </Row>
    );
  }

  renderAdvancedForm() {
    const {
      tableInfomation: { querys },
    } = this.props;
    if (querys) {
      const qs = querys.filter(t => t.type !== 'HiddenTextFieldAttribute');
      const hiddens = querys.filter(t => t.type === 'HiddenTextFieldAttribute');
      if (qs && qs.length > 0) {
        const rows = [];
        for (let i = 0; i < qs.length / 3; i += 1) {
          if (qs[i * 3]) {
            const cols = [];
            cols.push({ data: this.createFormIten(qs[i * 3]), key: i * 3 });
            if (qs[i * 3 + 1]) {
              cols.push({ data: this.createFormIten(qs[i * 3 + 1]), key: i * 3 + 1 });
            }
            if (qs[i * 3 + 2]) {
              cols.push({ data: this.createFormIten(qs[i * 3 + 2]), key: i * 3 + 2 });
            }
            rows.push({ data: cols, key: i });
          }
        }
        if (rows) {
          return (
            <Form onSubmit={this.handleSearch} layout="inline">
              {hiddens && hiddens.map(r => <div key={r.id}>{this.createFormIten(r)}</div>)}
              {rows.map(r => (
                <Row key={r.key} gutter={{ md: 8, lg: 24, xl: 48 }}>
                  {r.data.map(c => (
                    <Col key={c.key} md={8} sm={24}>
                      {c.data}
                    </Col>
                  ))}
                </Row>
              ))}
              <div style={{ overflow: 'hidden' }}>
                <div style={{ float: 'right', marginBottom: 24 }}>
                  <Button type="primary" htmlType="submit">
                    查询
                  </Button>
                  {this.viewButton()}
                  <a style={{ marginLeft: 8 }} onClick={this.toggleForm}>
                    收起 <Icon type="up" />
                  </a>
                </div>
              </div>
            </Form>
          );
        }
      }
    }
    return <div />;
  }

  renderForm() {
    const { expandForm } = this.state;
    return expandForm ? this.renderAdvancedForm() : this.renderSimpleForm();
  }

  render() {
    const {
      rows: { data },
      tableInfomation,
      loading,
    } = this.props;
    const { selectedRows, modalVisible, updateModalVisible, stepFormValues, modalId } = this.state;
    const menu = (
      <Menu onClick={this.handleMenuClick} selectedKeys={[]}>
        <Menu.Item key="remove">删除</Menu.Item>
        <Menu.Item key="approval">批量审批</Menu.Item>
      </Menu>
    );

    const parentMethods = {
      handleAdd: this.handleAdd,
      handleModalVisible: this.handleModalVisible,
    };
    const updateMethods = {
      handleUpdateModalVisible: this.handleUpdateModalVisible,
      handleUpdate: this.handleUpdate,
    };
    return (
      <PageHeaderWrapper title={tableInfomation.tableName}>
        <Card bordered={false}>
          <div className={styles.tableList}>
            <div className={styles.tableListForm}>{this.renderForm()}</div>
            <div className={styles.tableListOperator}>
              {selectedRows.length > 0 && (
                <span>
                  <Button>批量操作</Button>
                  <Dropdown overlay={menu}>
                    <Button>
                      更多操作 <Icon type="down" />
                    </Button>
                  </Dropdown>
                </span>
              )}
            </div>
            <StandardTable
              selectedRows={selectedRows}
              loading={loading}
              data={data}
              columns={tableInfomation.data}
              onSelectRow={this.handleSelectRows}
              onChange={this.handleStandardTableChange}
            />
          </div>
        </Card>
        <CreateForm {...parentMethods} modalVisible={modalVisible} modalId={modalId} />
        {stepFormValues && Object.keys(stepFormValues).length ? (
          <UpdateForm
            {...updateMethods}
            updateModalVisible={updateModalVisible}
            values={stepFormValues}
          />
        ) : null}
      </PageHeaderWrapper>
    );
  }
}

export default ZzbTableListDetail;
