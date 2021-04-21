import { post } from '@/zzb/utils/request';

export default {
  namespace: 'backReport',

  state: {
    players: { allCoin: 0, allPlayer: 0, proxyPlayer: 0, memberPlayer: 0, onlinePlayer: 0 },
    allReport: {
      allRechargeCoin: 0,
      allWithdrawCoin: 0,
      allBetMoney: 0,
      allWinMoney: 0,
      allGiftMoney: 0,
      allRechargeWinMoney: 0,
      allBetWinMoney: 0,
      allRebate: 0,
      lotteryTypes: [],
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
    *getAllReport({ begin, end }, { call, put }) {
      const res = yield call(post, 'api/BackReport/GetAllReport', { begin, end });
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
