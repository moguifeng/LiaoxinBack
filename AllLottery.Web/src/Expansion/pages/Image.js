import React, { Component } from 'react';
import { Upload, Modal, Icon } from 'antd';

class Image extends Component {
  state = {
    fileList: [],
    previewVisible: false,
    previewImage: '',
  };

  componentDidMount() {
    const { value } = this.props;
    if (value) {
      const fileList = [
        {
          uid: value,
          name: '设置',
          status: 'done',
          url: `${window.host}/api/Image/GetAffix?id=${value}`,
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

  handlePictureChange = ({ fileList }) => {
    const { callBack } = this.props;
    if (callBack) {
      if (fileList && fileList.length > 0) {
        if (fileList[0]) {
          if (fileList[0].response) {
            callBack(fileList[0].response.id);
          }
        } else {
          callBack(null);
        }
      } else {
        callBack(null);
      }
    }
    this.setState({ fileList });
  };

  handleCancel = () => this.setState({ previewVisible: false });

  render() {
    const { fileList, previewVisible, previewImage } = this.state;
    const uploadButton = (
      <div>
        <Icon type="plus" />
        <div className="ant-upload-text">Upload</div>
      </div>
    );
    return (
      <div className="clearfix" style={{ width: 112, margin: 'auto', textAlign: 'center' }}>
        <Upload
          action={`${window.host}/api/Image/UploadImage`}
          listType="picture-card"
          fileList={fileList}
          onPreview={this.handlePreview}
          onChange={f => this.handlePictureChange(f)}
        >
          {fileList.length >= 1 ? null : uploadButton}
        </Upload>
        <Modal visible={previewVisible} footer={null} onCancel={this.handleCancel}>
          <img alt="example" style={{ width: '100%' }} src={previewImage} />
        </Modal>
      </div>
    );
  }
}

export default Image;
