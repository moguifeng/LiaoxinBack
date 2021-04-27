/* eslint-disable guard-for-in */
import React, { PureComponent } from 'react';
import { Form, Card, Row, Col, Input, Button } from 'antd';
import PageHeaderWrapper from '@/components/PageHeaderWrapper';
import { connect } from 'dva';
import styles from './style.less';

@connect(({ systemConfig }) => ({
  systemConfig,
}))
@Form.create()
class SystemConfig extends PureComponent {
  componentDidMount() {
    const { dispatch } = this.props;
    dispatch({
      type: 'systemConfig/getSystemConfigSettings',
    });
  }

  save(names) {
    const { form, dispatch } = this.props;
    form.validateFields((err, fieldsValue) => {
      if (err) return;
      const data = {};
      // eslint-disable-next-line guard-for-in
      // eslint-disable-next-line no-restricted-syntax
      for (const key in names) {
        data[names[key]] = fieldsValue[names[key]];
      }
      dispatch({
        type: 'systemConfig/saveConfigs',
        data,
      });
    });
  }

  writeItems(items) {
    const rows = [];
    const names = items.map(t => t.name);
    for (let i = 0; i < items.length / 3; i += 1) {
      let exist = 0;
      const cols = [];
      if (items[i * 3]) {
        cols.push({
          data: (
            <Col key={items[i * 3].name} lg={6} md={12} sm={24}>
              {this.createFormItem(items[i * 3])}
            </Col>
          ),
        });
        exist += 1;
      }
      if (items[i * 3 + 1]) {
        cols.push({
          data: (
            <Col
              key={items[i * 3 + 1].name}
              xl={{ span: 6, offset: 2 }}
              lg={{ span: 8 }}
              md={{ span: 12 }}
              sm={24}
            >
              {this.createFormItem(items[i * 3 + 1])}
            </Col>
          ),
        });
        exist += 1;
      }
      if (items[i * 3 + 2]) {
        cols.push({
          data: (
            <Col
              key={items[i * 3 + 2].name}
              xl={{ span: 6, offset: 2 }}
              lg={{ span: 8 }}
              md={{ span: 12 }}
              sm={24}
            >
              {this.createFormItem(items[i * 3 + 2])}
            </Col>
          ),
        });
        exist += 1;
      }
      if (exist < 2) {
        cols.push({
          data: (
            <Col key="123" xl={{ span: 6, offset: 2 }} lg={{ span: 8 }} md={{ span: 12 }} sm={24} />
          ),
        });
      }
      if (exist < 3) {
        cols.push({
          data: (
            <Col key="234" xl={{ span: 8, offset: 2 }} lg={{ span: 10 }} md={{ span: 24 }} sm={24}>
              <Button
                style={{ marginTop: 29 }}
                type="primary"
                onClick={() => {
                  this.save(names);
                }}
              >
                保存
              </Button>
            </Col>
          ),
        });
      }
      rows.push({ data: cols, i });
    }
    if (items.length % 3 === 0) {
      const cols = [];
      cols.push({ data: <Col key="1" lg={6} md={12} sm={24} /> });
      cols.push({
        data: (
          <Col key="2" xl={{ span: 6, offset: 2 }} lg={{ span: 8 }} md={{ span: 12 }} sm={24} />
        ),
      });
      cols.push({
        data: (
          <Col key="234" xl={{ span: 8, offset: 2 }} lg={{ span: 10 }} md={{ span: 24 }} sm={24}>
            <Button
              style={{ marginTop: 29 }}
              type="primary"
              onClick={() => {
                this.save(names);
              }}
            >
              保存
            </Button>
          </Col>
        ),
      });
      rows.push({ data: cols, i: items.length / 3 + 1 });
    }
    return rows.map(t => (
      <Row key={t.i} gutter={16}>
        {t.data.map(i => i.data)}
      </Row>
    ));
  }

  createFormItem(item) {
    const {
      form: { getFieldDecorator },
    } = this.props;
    const message = `请输入${item.title}`;
    return (
      <Form.Item key={item.name} label={item.title}>
        {getFieldDecorator(item.name, {
          rules: [{ required: false, message }],
          initialValue: item.value,
        })(<Input key={item.name} placeholder={message} />)}
      </Form.Item>
    );
  }

  render() {
    const {
      systemConfig: { keys },
    } = this.props;
    return (
      <PageHeaderWrapper title="系统配置" content="配置一些系统的参数。">
        {keys.map(t => (
          <Card key={t.name} title={t.name} className={styles.card} bordered={false}>
            <Form key={t.name} layout="vertical" hideRequiredMark>
              {this.writeItems(t.items)}
            </Form>
          </Card>
        ))}
      </PageHeaderWrapper>
    );
  }
}

export default SystemConfig;
