import { post } from '@/zzb/utils/request';

export default {
  namespace: 'player',

  state: {
    onlineCount: 0,
  },

  effects: {
    *updatePlayerOnlineCount(_, { call, put }) {
      const count = yield call(post, 'api/Home/GetPlayerOnlineCount');
      yield put({ type: 'updatePlayerOnlineCountSave', count });
    },
  },

  reducers: {
    updatePlayerOnlineCountSave(state, action) {
      return {
        ...state,
        onlineCount: action.count,
      };
    },
  },
};
