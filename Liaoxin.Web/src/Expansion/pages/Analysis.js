import React, { PureComponent } from 'react';
import PageHeaderWrapper from '@/components/PageHeaderWrapper';
import { Card, Table, DatePicker, Button } from 'antd';
import { connect } from 'dva';
import locale from 'antd/lib/date-picker/locale/en_US';
import 'moment/locale/zh-cn';

const { RangePicker } = DatePicker;

const gridStyle = {
  width: '20%',
  textAlign: 'center',
};

const gridStyle1 = {
  width: '12.5%',
  textAlign: 'center',
};

@connect(({ backReport }) => ({
  backReport,
}))
class Analysis extends PureComponent {
  state = {
    begin: '',
    end: '',
  };

  componentDidMount() {
    // const { dispatch } = this.props;
    // dispatch({
    //   type: 'backReport/getPlayerReport',
    // });
    // dispatch({
    //   type: 'backReport/getAllReport',
    // });
  }

  onRangePickerChange(_, data) {
    this.setState({
      begin: data[0],
      end: data[1],
    });
  }

  onQuery = () => {
    const { dispatch } = this.props;
    const { begin, end } = this.state;
    dispatch({
      type: 'backReport/getAllReport',
      begin,
      end,
    });
  };

  accAdd = (arg1, arg2) => {
    let r1;
    let r2;
    try {
      r1 = arg1.toString().split('.')[1].length;
    } catch (e) {
      r1 = 0;
    }
    try {
      r2 = arg2.toString().split('.')[1].length;
    } catch (e) {
      r2 = 0;
    }
    // eslint-disable-next-line no-restricted-properties
    const m = Math.pow(10, Math.max(r1, r2));
    return (arg1 * m + arg2 * m) / m;
  };

  Subtr = (arg1, arg2) => {
    let r1;
    let r2;
    try {
      r1 = arg1.toString().split('.')[1].length;
    } catch (e) {
      r1 = 0;
    }
    try {
      r2 = arg2.toString().split('.')[1].length;
    } catch (e) {
      r2 = 0;
    }
    // eslint-disable-next-line no-restricted-properties
    const m = Math.pow(10, Math.max(r1, r2));
    const n = r1 >= r2 ? r1 : r2;
    return ((arg1 * m - arg2 * m) / m).toFixed(n);
  };

  render() {
    const {
      backReport: { players, allReport },
    } = this.props;
    let goodsData = [];
    if (allReport.lotteryTypes.length) {
      let betMoney = 0;
      let winMoney = 0;
      allReport.lotteryTypes.forEach(item => {
        betMoney = this.accAdd(betMoney, item.betMoney);
        winMoney = this.accAdd(winMoney, item.winMoney);
      });
      goodsData = allReport.lotteryTypes.concat({
        id: '总计',
        betMoney,
        winMoney,
      });
    }
    // const renderContent = (value, row, index) => {
    //   const obj = {
    //     children: value,
    //     props: {},
    //   };
    //   if (index === allReport.lotteryTypes.length) {
    //     obj.props.colSpan = 0;
    //   }
    //   return obj;
    // };
    const goodsColumns = [
      //   {
      //     title: 'ID',
      //     dataIndex: 'id',
      //     key: 'id',
      //     align: 'center',
      //     render: renderContent,
      //   },
      {
        title: '群号',
        dataIndex: 'name',
        key: 'name',
        align: 'center',
        render: (text, row, index) => {
          if (index < allReport.lotteryTypes.length) {
            return <span>{text}</span>;
          }
          return {
            children: <span style={{ fontWeight: 600 }}>总计</span>,
          };
        },
      },

      {
        title: '尾数',
        dataIndex: 'betMoney',
        key: 'betMoney',
        align: 'center',
        render: (text, row, index) => {
          if (index < allReport.lotteryTypes.length) {
            return text;
          }
          return <span style={{ fontWeight: 600 }}>{text}</span>;
        },
      },

      {
        title: '群红包是否中奖',
        dataIndex: 'betMoney',
        key: 'betMoney',
        align: 'center',
        render: (text, row, index) => {
          if (index < allReport.lotteryTypes.length) {
            return text;
          }
          return <span style={{ fontWeight: 600 }}>{text}</span>;
        },
      },
      {
        title: '红包中奖人',
        dataIndex: 'winMoney',
        key: 'winMoney',
        align: 'center',
        render: (text, row, index) => {
          if (index < allReport.lotteryTypes.length) {
            return text;
          }
          return <span style={{ fontWeight: 600 }}>{text}</span>;
        },
      },
      {
        title: '红包中奖金额',
        dataIndex: 'allWinMoney',
        key: 'allWinMoney',
        align: 'center',
        render: (text, row) => this.Subtr(row.betMoney, row.winMoney),
      },
    ];
    return (
      <PageHeaderWrapper title="统计概况" content="统计网站数据。">
        <Card title="用户统计" style={{ marginBottom: 24 }}>
          <Card.Grid style={gridStyle}>
            <Card bodyStyle={{ padding: 0 }} bordered={false}>
              <Card.Meta title="客户总数" description={<div>{players.allPlayer}</div>} />
            </Card>
          </Card.Grid>

          <Card.Grid style={gridStyle}>
            <Card bodyStyle={{ padding: 0 }} bordered={false}>
              <Card.Meta title="群总数" description={<div>{players.allPlayer}</div>} />
            </Card>
          </Card.Grid>

        
        </Card>

        <div>
          <RangePicker
            locale={locale}
            style={{ marginBottom: 10 }}
            onChange={(_, data) => {
              this.setState({
                begin: data[0],
                end: data[1],
              });
            }}
          />
          <Button type="primary" style={{ float: 'right' }} onClick={this.onQuery}>
            查询
          </Button>
        </div>

        <Card
          title="亏盈统计"
          extra={<p> 充提盈亏 = 充值金额-提款金额</p>}
          style={{ marginBottom: 24 }}
        >
          <Card.Grid style={gridStyle1}>
            <Card bodyStyle={{ padding: 0 }} bordered={false}>
              <Card.Meta title="充值" description={<div>{allReport.allRechargeCoin}</div>} />
            </Card>
          </Card.Grid>
          <Card.Grid style={gridStyle1}>
            <Card bodyStyle={{ padding: 0 }} bordered={false}>
              <Card.Meta title="提款" description={<div>{allReport.allWithdrawCoin}</div>} />
            </Card>
          </Card.Grid>
         
          <Card.Grid style={gridStyle1}>
            <Card bodyStyle={{ padding: 0 }} bordered={false}>
              <Card.Meta title="红包中奖个数" description={<div>{allReport.allWinMoney}</div>} />
            </Card>
          </Card.Grid>
          
          <Card.Grid style={gridStyle1}>
            <Card bodyStyle={{ padding: 0 }} bordered={false}>
              <Card.Meta
                title="充提盈亏"
                description={<div>{allReport.allRechargeWinMoney}</div>}
              />
            </Card>
          </Card.Grid>
   
        </Card>
        <Card bordered={false}>
          <Table
            style={{ marginBottom: 24 }}
            pagination={false}
            dataSource={goodsData}
            columns={goodsColumns}
            rowKey="id"
          />
        </Card>
      </PageHeaderWrapper>
    );
  }
}

export default Analysis;
