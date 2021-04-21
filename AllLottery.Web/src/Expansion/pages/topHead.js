import React, { PureComponent } from 'react';
import { connect } from 'dva';

@connect(({ player }) => ({
  player,
}))
class TopHead extends PureComponent {
  render() {
    const { player } = this.props;
    return (
      <span>
        <a
          href="/ZzbTable/a6611074f5d2354b96279cc92aaede4f/79c3ad9d177e27d4a08dfeb1c3ad9c69"
          style={{ marginRight: 20 }}
        >
          提款管理
        </a>
        <a
          href="/ZzbTable/a335eed50b930fd1f6a03a50edf33672/b8b007ad92975148605c48728f15598b"
          style={{ marginRight: 20 }}
        >
          充值管理
        </a>
        <a
          href="/ZzbTable/0655aa0f5cae24c5e7a2091881058db1/19f9182e9ecc542bcbdd3f25ee04917c"
          style={{ marginRight: 20 }}
        >
          玩家管理
        </a>
        <span style={{ marginRight: 10 }}>平台在线人数：{player?.onlineCount}</span>
      </span>
    );
  }
}

export default TopHead;
