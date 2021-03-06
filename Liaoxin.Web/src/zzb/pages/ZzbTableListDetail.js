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
        <FormItem key="target" {...this.formLayout} label="????????????">
          {form.getFieldDecorator('target', {
            initialValue: formVals.target,
          })(
            <Select style={{ width: '100%' }}>
              <Option value="0">??????</Option>
              <Option value="1">??????</Option>
            </Select>
          )}
        </FormItem>,
        <FormItem key="template" {...this.formLayout} label="????????????">
          {form.getFieldDecorator('template', {
            initialValue: formVals.template,
          })(
            <Select style={{ width: '100%' }}>
              <Option value="0">???????????????</Option>
              <Option value="1">???????????????</Option>
            </Select>
          )}
        </FormItem>,
        <FormItem key="type" {...this.formLayout} label="????????????">
          {form.getFieldDecorator('type', {
            initialValue: formVals.type,
          })(
            <RadioGroup>
              <Radio value="0">???</Radio>
              <Radio value="1">???</Radio>
            </RadioGroup>
          )}
        </FormItem>,
      ];
    }
    if (currentStep === 2) {
      return [
        <FormItem key="time" {...this.formLayout} label="????????????">
          {form.getFieldDecorator('time', {
            rules: [{ required: true, message: '????????????????????????' }],
          })(
            <DatePicker
              style={{ width: '100%' }}
              showTime
              format="YYYY-MM-DD HH:mm:ss"
              placeholder="??????????????????"
            />
          )}
        </FormItem>,
        <FormItem key="frequency" {...this.formLayout} label="????????????">
          {form.getFieldDecorator('frequency', {
            initialValue: formVals.frequency,
          })(
            <Select style={{ width: '100%' }}>
              <Option value="month">???</Option>
              <Option value="week">???</Option>
            </Select>
          )}
        </FormItem>,
      ];
    }
    return [
      <FormItem key="name" {...this.formLayout} label="????????????">
        {form.getFieldDecorator('name', {
          rules: [{ required: true, message: '????????????????????????' }],
          initialValue: formVals.name,
        })(<Input placeholder="?????????" />)}
      </FormItem>,
      <FormItem key="desc" {...this.formLayout} label="????????????">
        {form.getFieldDecorator('desc', {
          rules: [{ required: true, message: '?????????????????????????????????????????????', min: 5 }],
          initialValue: formVals.desc,
        })(<TextArea rows={4} placeholder="???????????????????????????" />)}
      </FormItem>,
    ];
  };

  renderFooter = currentStep => {
    const { handleUpdateModalVisible, values } = this.props;
    if (currentStep === 1) {
      return [
        <Button key="back" style={{ float: 'left' }} onClick={this.backward}>
          ?????????
        </Button>,
        <Button key="cancel" onClick={() => handleUpdateModalVisible(false, values)}>
          ??????
        </Button>,
        <Button key="forward" type="primary" onClick={() => this.handleNext(currentStep)}>
          ?????????
        </Button>,
      ];
    }
    if (currentStep === 2) {
      return [
        <Button key="back" style={{ float: 'left' }} onClick={this.backward}>
          ?????????
        </Button>,
        <Button key="cancel" onClick={() => handleUpdateModalVisible(false, values)}>
          ??????
        </Button>,
        <Button key="submit" type="primary" onClick={() => this.handleNext(currentStep)}>
          ??????
        </Button>,
      ];
    }
    return [
      <Button key="cancel" onClick={() => handleUpdateModalVisible(false, values)}>
        ??????
      </Button>,
      <Button key="forward" type="primary" onClick={() => this.handleNext(currentStep)}>
        ?????????
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
        title="????????????"
        visible={updateModalVisible}
        footer={this.renderFooter(currentStep)}
        onCancel={() => handleUpdateModalVisible(false, values)}
        afterClose={() => handleUpdateModalVisible()}
      >
        <Steps style={{ marginBottom: 28 }} size="small" current={currentStep}>
          <Step title="????????????" />
          <Step title="??????????????????" />
          <Step title="??????????????????" />
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
        okText: '??????',
        cancelText: '??????',
        maskClosable: true,
      });
    }
    console.log(button);
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

    message.success('????????????');
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

    message.success('????????????');
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
                    ??????
                  </Button>
                  {this.viewButton()}
                  {qs.length > 2 && (
                    <a style={{ marginLeft: 8 }} onClick={this.toggleForm}>
                      ?????? <Icon type="down" />
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
              ??????
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
                    ??????
                  </Button>
                  {this.viewButton()}
                  <a style={{ marginLeft: 8 }} onClick={this.toggleForm}>
                    ?????? <Icon type="up" />
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
        <Menu.Item key="remove">??????</Menu.Item>
        <Menu.Item key="approval">????????????</Menu.Item>
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
                  <Button>????????????</Button>
                  <Dropdown overlay={menu}>
                    <Button>
                      ???????????? <Icon type="down" />
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
