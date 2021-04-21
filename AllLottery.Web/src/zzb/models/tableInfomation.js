import getColumns from '@/zzb/services/tableInfomation';
import React, { Fragment } from 'react';
import zzbAction from '../action';

export default {
  namespace: 'tableInfomation',

  state: {
    data: [],
    viewButton: [],
    querys: [],
  },

  effects: {
    *getInfomation({ navId, handelButton, callBack, form, query }, { call, put }) {
      yield put({ type: 'save', data: [], viewButton: [], tableName: null, querys: [] });
      const res = yield call(getColumns, navId);
      const columns = res.navColumns
        .filter(item => !item.isHidden)
        .map(item => ({
          title: item.title,
          dataIndex: item.name,
          align: 'center',
          width: item.width,
          // eslint-disable-next-line react/no-danger
          render: (text, record) => {
            if (item.actionType) {
              return (
                <a
                  style={{
                    width: item.width,
                    margin: 'auto',
                    overflow: 'hidden',
                    whiteSpace: 'nowrap',
                    textOverflow: 'ellipsis',
                  }}
                  onClick={() => {
                    zzbAction(item.actionType, { record, form, query });
                  }}
                >
                  {text}
                </a>
              );
            }
            return (
              <div
                style={{
                  width: item.width,
                  margin: 'auto',
                  overflow: 'hidden',
                  whiteSpace: 'nowrap',
                  textOverflow: 'ellipsis',
                }}
                title={text}
                dangerouslySetInnerHTML={{ __html: text }}
              />
            );
          },
        }));
      if (res.showOperaColumn) {
        columns.push({
          title: '操作',
          dataIndex: 'Zzb_Buttons',
          width: res.operaColumnWidth,
          fixed: 'right',
          render: (text, record) => (
            <div style={{ width: res.operaColumnWidth }}>
              <Fragment>
                {text &&
                  text.map(t => (
                    <a
                      style={{ marginRight: '10px' }}
                      key={t.id}
                      onClick={() => handelButton(t, { ...record, Zzb_Buttons: null })}
                    >
                      {t.buttonName}
                    </a>
                  ))}
              </Fragment>
            </div>
          ),
        });
      }
      yield put({
        type: 'save',
        data: columns,
        tableName: res.tableName,
        querys: res.queryFields,
        viewButton: res.viewButtons,
      });
      if (callBack) {
        callBack();
      }
    },
  },

  reducers: {
    save(state, action) {
      return {
        ...state,
        ...action,
      };
    },
  },
};
