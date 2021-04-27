import React, { PureComponent } from 'react';
import { FormattedMessage, formatMessage } from 'umi/locale';
import { Spin, Tag, Menu, Icon, Avatar, Modal, Button, Input, Form } from 'antd';
import moment from 'moment';
import groupBy from 'lodash/groupBy';
import { connect } from 'dva';
import NoticeIcon from '../NoticeIcon';
import HeaderDropdown from '../HeaderDropdown';
import styles from './index.less';
import TopHead from '@/Expansion/pages/topHead';

const FormItem = Form.Item;

@connect(({ user, player }) => ({
  user,
  player,
}))
@Form.create()
class GlobalHeaderRight extends PureComponent {
  state = {
    visible: false,
  };

  getNoticeData() {
    const { notices = [] } = this.props;
    if (notices.length === 0) {
      return {};
    }
    const newNotices = notices.map(notice => {
      const newNotice = { ...notice };
      if (newNotice.datetime) {
        newNotice.datetime = moment(notice.datetime).fromNow();
      }
      if (newNotice.id) {
        newNotice.key = newNotice.id;
      }
      if (newNotice.extra && newNotice.status) {
        const color = {
          todo: '',
          processing: 'blue',
          urgent: 'red',
          doing: 'gold',
        }[newNotice.status];
        newNotice.extra = (
          <Tag color={color} style={{ marginRight: 0 }}>
            {newNotice.extra}
          </Tag>
        );
      }
      return newNotice;
    });
    return groupBy(newNotices, 'type');
  }

  showModal = () => {
    this.setState({
      visible: true,
    });
  };

  handleCancel = () => {
    this.setState({
      visible: false,
    });
  };

  getUnreadData = noticeData => {
    const unreadMsg = {};
    Object.entries(noticeData).forEach(([key, value]) => {
      if (!unreadMsg[key]) {
        unreadMsg[key] = 0;
      }
      if (Array.isArray(value)) {
        unreadMsg[key] = value.filter(item => !item.read).length;
      }
    });
    return unreadMsg;
  };

  changeReadState = clickedItem => {
    const { id } = clickedItem;
    const { dispatch } = this.props;
    dispatch({
      type: 'global/changeNoticeReadState',
      payload: id,
    });
  };

  save = () => {
    const { form, dispatch } = this.props;
    form.validateFields((err, fieldsValue) => {
      if (err) return;
      dispatch({
        type: 'user/changePassword',
        data: fieldsValue,
        cancel: this.handleCancel,
      });
    });
  };

  render() {
    const {
      currentUser,
      fetchingNotices,
      onNoticeVisibleChange,
      onMenuClick,
      onNoticeClear,
      theme,
      form,
    } = this.props;
    const { visible } = this.state;
    const { showModal } = this;
    const menu = (
      <Menu
        className={styles.menu}
        selectedKeys={[]}
        onClick={k => {
          if (k.key === 'logout') onMenuClick(k);
          if (k.key === 'key') {
            showModal();
          }
        }}
      >
        <Menu.Item key="key">
          <Icon type="key" />
          修改密码
        </Menu.Item>
        <Menu.Item key="logout">
          <Icon type="logout" />
          <FormattedMessage id="menu.account.logout" defaultMessage="logout" />
        </Menu.Item>
      </Menu>
    );
    const noticeData = this.getNoticeData();
    const unreadMsg = this.getUnreadData(noticeData);
    let className = styles.right;
    if (theme === 'dark') {
      className = `${styles.right}  ${styles.dark}`;
    }
    return (
      <div className={`${className} zzbbianjie`}>
        <TopHead />
        <NoticeIcon
          className={styles.action}
          count={currentUser.unreadCount}
          onItemClick={(item, tabProps) => {
            console.log(item, tabProps); // eslint-disable-line
            this.changeReadState(item, tabProps);
          }}
          locale={{
            emptyText: formatMessage({ id: 'component.noticeIcon.empty' }),
            clear: formatMessage({ id: 'component.noticeIcon.clear' }),
          }}
          onClear={onNoticeClear}
          onPopupVisibleChange={onNoticeVisibleChange}
          loading={fetchingNotices}
          clearClose
        >
          <NoticeIcon.Tab
            count={unreadMsg.notification}
            list={noticeData.notification}
            title={formatMessage({ id: 'component.globalHeader.notification' })}
            name="notification"
            emptyText={formatMessage({ id: 'component.globalHeader.notification.empty' })}
            emptyImage="https://gw.alipayobjects.com/zos/rmsportal/wAhyIChODzsoKIOBHcBk.svg"
          />
        </NoticeIcon>
        {currentUser.name ? (
          <HeaderDropdown overlay={menu}>
            <span className={`${styles.action} ${styles.account}`}>
              <Avatar
                size="small"
                className={styles.avatar}
                src={currentUser.avatar}
                alt="avatar"
              />
              <span className={styles.name}>{currentUser.name}</span>
            </span>
          </HeaderDropdown>
        ) : (
          <Spin size="small" style={{ marginLeft: 8, marginRight: 8 }} />
        )}
        <Modal
          visible={visible}
          title="修改密码"
          onCancel={this.handleCancel}
          footer={[
            <Button key="submit" type="primary" onClick={this.save}>
              保存
            </Button>,
            <Button key="back" onClick={this.handleCancel}>
              取消
            </Button>,
          ]}
        >
          <FormItem labelCol={{ span: 5 }} wrapperCol={{ span: 15 }} label="原密码">
            {form.getFieldDecorator('password', {
              rules: [{ required: true, message: `请输入原密码` }],
            })(<Input type="password" placeholder="请输入原密码" />)}
          </FormItem>
          <FormItem labelCol={{ span: 5 }} wrapperCol={{ span: 15 }} label="新密码">
            {form.getFieldDecorator('newPassword', {
              rules: [{ required: true, message: `请输入新密码` }],
            })(<Input type="password" placeholder="请输入新密码" />)}
          </FormItem>
          <FormItem labelCol={{ span: 5 }} wrapperCol={{ span: 15 }} label="确定密码">
            {form.getFieldDecorator('surePassword', {
              rules: [
                { required: true, message: `请输入确定密码` },
                {
                  validator: (_, value, callback) => {
                    if (value && value !== form.getFieldValue('newPassword')) {
                      callback('两次输入不一致！');
                    }
                    // Note: 必须总是返回一个 callback，否则 validateFieldsAndScroll 无法响应
                    callback();
                  },
                },
              ],
            })(<Input type="password" placeholder="请输入确定密码" />)}
          </FormItem>
        </Modal>
      </div>
    );
  }
}

export default GlobalHeaderRight;
