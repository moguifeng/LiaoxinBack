import React, { PureComponent } from 'react';
import PageHeaderWrapper from '@/components/PageHeaderWrapper';
import { Card } from 'antd';
import { connect } from 'dva';
import Image from './Image';

const gridStyle = {
  width: '25%',
  textAlign: 'center',
};

@connect(({ systemConfig }) => ({
  systemConfig,
}))
class ImageConfig extends PureComponent {
  componentDidMount() {
    const { dispatch } = this.props;
    dispatch({
      type: 'systemConfig/getImageSetting',
    });
  }

  Save = (id, value) => {
    const { dispatch } = this.props;
    const data = {};
    data[id] = value;
    dispatch({
      type: 'systemConfig/saveConfigs',
      data,
    });
  };

  render() {
    const {
      systemConfig: { images },
    } = this.props;

    return (
      <PageHeaderWrapper title="图片配置" content="配置一些系统的图片。">
        <Card>
          {images.map(t => (
            <Card.Grid key={t.name} style={gridStyle}>
              <Image
                callBack={k => {
                  this.Save(t.name, k);
                }}
                value={t.value}
              />
              <div>{t.title}</div>
            </Card.Grid>
          ))}
        </Card>
      </PageHeaderWrapper>
    );
  }
}

export default ImageConfig;
