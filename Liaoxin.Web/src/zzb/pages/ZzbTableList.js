import React, { PureComponent } from 'react';
import ZzbTableListDetail from './ZzbTableListDetail';

class ZzbTableList extends PureComponent {
  render() {
    const {
      match: {
        params: { id },
      },
    } = this.props;
    return <ZzbTableListDetail {...this.props} id={id} key={id} />;
  }
}

export default ZzbTableList;
