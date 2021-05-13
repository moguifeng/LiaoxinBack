import { post } from '@/zzb/utils/request';

export default {
  namespace: 'backReport',

  state: {    
    allReport: {
      groupCount: 0,
      clientCount: 0,
      withdraws: 0,
      recharges: 0,
      backReports: [],
    },
  },

  effects: {
    *getPlayerReport(_, { call, put }) {
      const res = yield call(post, 'api/BackReport/GetPlayerReport');
      yield put({
        type: 'save',
        players: res,
      });
    },
    *getAllReport({ begin, end,realName,liaoxinNumber }, { call, put }) {
      const res = yield call(post, 'api/BackReport/GetAllReport', { begin, end ,realName,liaoxinNumber});
      yield put({
        type: 'save',
        allReport: res,
      });
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
