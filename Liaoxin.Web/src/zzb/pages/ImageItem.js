import React, { Component } from 'react';
import { Upload, Modal, Icon, Input } from 'antd';

class ImagerItem extends Component {
  state = {
    fileList: [],
    previewVisible: false,
    previewImage: '',
  };

  componentDidMount() {
    const { field } = this.props;
    if (field.value) {
      const fileList = [
        {
          uid: field.value,
          name: field.title,
          status: 'done',
          url: `${window.host}/api/Image/GetAffix?id=${field.value}`,
        },
      ];
      this.setState({ fileList });
    }
  }

  handlePreview = file => {
    this.setState({
      previewImage: file.url || file.thumbUrl,
      previewVisible: true,
    });
  };

  handlePictureChange = ({ fileList }, id) => {
    const { handleChange } = this.props;
    if (fileList && fileList[0] && fileList[0].response) {
      handleChange(fileList[0].response.id, id);
    } else {
      handleChange(null, id);
    }
    this.setState({ fileList });
  };

  handleCancel = () => this.setState({ previewVisible: false });

  render() {
    const { fileList, previewVisible, previewImage } = this.state;
    const { form, field, option } = this.props;
    let pla = field.title;
    if (field.placeholder) {
      pla = field.placeholder;
    }
    const uploadButton = (
      <div>
        <Icon type="plus" />
        <div className="ant-upload-text">Upload</div>
      </div>
    );
    return (
      <div className="clearfix">
        {form.getFieldDecorator(field.id, { ...option, initialValue: field.value })(
          <Input type="hidden" placeholder={pla} disabled={!!field.isReadOnly} />
        )}
        <Upload
          action={`${window.host}/api/Image/UploadImage`}
          listType="picture-card"
          fileList={fileList}
          onPreview={this.handlePreview}
          onChange={f => this.handlePictureChange(f, field.id)}
        >
          {fileList.length >= 1 ? null : uploadButton}
        </Upload>
        <Modal visible={previewVisible} footer={null} onCancel={this.handleCancel}>
          <img
            alt="example"
            style={{ width: '100%', backgroundColor: field.backColor }}
            src={previewImage}
          />
        </Modal>
      </div>
    );
  }
}

export default ImagerItem;
