import React, { PureComponent } from 'react';
import PageHeaderWrapper from '@/components/PageHeaderWrapper';
import { Card, Table, DatePicker, Button, Input } from 'antd';
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
    realName:'',
    liaoxinNumber:''
  };

  componentDidMount() {
     const { dispatch } = this.props;
    // dispatch({
    //   type: 'backReport/getPlayerReport',
    // });
    dispatch({
      type: 'backReport/getAllReport',
    });
  }

  onRangePickerChange(_, data) {
    this.setState({
      begin: data[0],
      end: data[1],
    });
  }

  onQuery = () => {
    const { dispatch } = this.props;
    const { begin, end ,realName,liaoxinNumber} = this.state;
    dispatch({
      type: 'backReport/getAllReport',
      begin,
      end,
      realName,
      liaoxinNumber,
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
      backReport: {  allReport },
    } = this.props;
        
  const that = this;
    let goodsData = [];
 
      goodsData = allReport.backReports;
     
 
    const goodsColumns = [
      {
        title: '中奖人聊信号',
        dataIndex: 'liaoxinNumber',
        key: 'liaoxinNumber',
        align: 'center',
        render: (text, row, index) => {
          if (index < allReport.backReports.length) {
            return <span>{text}</span>;
          }
         
        },
      },

      {
        title: '中奖人姓名',
        dataIndex: 'realName',
        key: 'realName',
        align: 'center',
        render: (text, row, index) => {
          if (index < allReport.backReports.length) {
            return text;
          }
          return <span style={{ fontWeight: 600 }}>{text}</span>;
        },
      },

      {
        title: '中奖总金额',
        dataIndex: 'wins',
        key: 'wins',
        align: 'center',
        render: (text, row, index) => {
          if (index < allReport.backReports.length) {
            return text;
          }
          return <span style={{ fontWeight: 600 }}>{text}</span>;
        },
      },
    
    ];
    return (
      <PageHeaderWrapper title="统计概况" content="统计网站数据。">
        <Card title="用户统计" style={{ marginBottom: 24 }}>
          <Card.Grid style={gridStyle}>
            <Card bodyStyle={{ padding: 0 }} bordered={false}>
              <Card.Meta title="客户总数" description={<div>{allReport.clientCount}</div>} />
            </Card>
          </Card.Grid>

          <Card.Grid style={gridStyle}>
            <Card bodyStyle={{ padding: 0 }} bordered={false}>
              <Card.Meta title="群总数" description={<div>{allReport.groupCount}</div>} />
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
            <Input placeholder="姓名" style={{width:'250px',marginLeft:'10px'}}        onKeyUp={(data) => {                
              that.setState({
                realName:data.target.value,
            
              })}} />
                      <Input placeholder="聊信号" style={{width:'250px',marginLeft:'10px'}}        onKeyUp={(data) => {                
              that.setState({
                liaoxinNumber:data.target.value,
            
              })}} />
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
              <Card.Meta title="充值" description={<div>{allReport.recharges}</div>} />
            </Card>
          </Card.Grid>
          <Card.Grid style={gridStyle1}>
            <Card bodyStyle={{ padding: 0 }} bordered={false}>
              <Card.Meta title="提款" description={<div>{allReport.withdraws}</div>} />
            </Card>
          </Card.Grid>

          <Card.Grid style={gridStyle1}>
            <Card bodyStyle={{ padding: 0 }} bordered={false}>
              <Card.Meta title="红包中奖个数" description={<div>{allReport.wins}</div>} />
            </Card>
          </Card.Grid>

          <Card.Grid style={gridStyle1}>
            <Card bodyStyle={{ padding: 0 }} bordered={false}>
              <Card.Meta
                title="充提盈亏"
                description={<div>{allReport.recharges- allReport.withdraws}</div>}
              />
            </Card>
          </Card.Grid>

        </Card>
        <Card bordered={false} >
          <Table
            style={{ marginBottom: 24,overflow:'auto',maxHeight:500}}
            pagination={false}
            dataSource={goodsData}
            columns={goodsColumns}
            rowKey="liaoxinNumber"
          />
        </Card>
      </PageHeaderWrapper>
    );
  }
}

export default Analysis;
