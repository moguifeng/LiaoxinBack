import getRowsData from '@/zzb/services/rows';
import { post } from '../utils/request';

export default {
  namespace: 'rows',

  state: {
    data: {
      list: [],
      pagination: {
        current: 1,
        pageSize: 10,
        total: 0,
      },
    },
  },

  effects: {
    *getRowsData({ index = 1, size = 10, navId, query = {} }, { call, put }) {
      let current = index;
      yield put({
        type: 'save',
        list: [],
        pagination: {
          current: 1,
          pageSize: 10,
          total: 0,
        },
      });
      let res = yield call(getRowsData, navId, index, size, query);
      if (!res.rows && index > 1) {
        current = 1;
        res = yield call(getRowsData, navId, current, size, query);
      }
      yield put({
        type: 'save',
        data: {
          list: res.rows,
          pagination: {
            current,
            pageSize: size,
            total: res.total,
          },
        },
      });
    },

    *handleRowAction({ navId, buttonId, data, callBack }, { call }) {
      yield call(post, 'api/Home/HandleRowAction', { navId, buttonId, data }, () => {
        callBack();
      });
    },
  },

  reducers: {
    save(state, action) {
      return {
        ...state,
        data: action.data,
      };
    },
  },
};
