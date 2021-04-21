import React, { PureComponent } from 'react';
import { connect } from 'dva';
import { Form, Button, Modal } from 'antd';
import CreateFormItem from './CreateFormItem';

@connect(({ modal }) => ({
  modal,
}))
@Form.create()
class CreateForm extends PureComponent {
  onOk = button => {
    const { form, modalId, dispatch, handleModalVisible } = this.props;
    form.validateFields((err, fieldsValue) => {
      if (err) return;
      dispatch({
        type: 'modal/handleAction',
        buttonId: button.id,
        modalId,
        data: fieldsValue,
        callBack: () => {
          handleModalVisible(false, modalId);
        },
      });
    });
  };

  renderFooter = handleModalVisible => {
    const {
      modal: { buttons },
      modalId,
    } = this.props;
    let bh = [];
    if (buttons) {
      bh = buttons.map(t => (
        <Button key={t.id} type={t.type} icon={t.icon} onClick={() => this.onOk(t)}>
          {t.name}
        </Button>
      ));
    }
    bh.push(
      <Button key="cancel" onClick={() => handleModalVisible(false, modalId)}>
        取消
      </Button>
    );
    return bh;
  };

  render() {
    const {
      modalVisible,
      form,
      handleModalVisible,
      modal: { fields, title },
      modalId,
    } = this.props;
    return (
      <Modal
        destroyOnClose
        title={title}
        visible={modalVisible}
        footer={this.renderFooter(handleModalVisible)}
        // onOk={okHandle}
        onCancel={() => handleModalVisible(false, modalId)}
      >
        {fields && fields.map(f => <CreateFormItem key={f.id} form={form} field={f} />)}
      </Modal>
    );
  }
}

export default CreateForm;
