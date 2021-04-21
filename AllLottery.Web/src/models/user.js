import { query as queryUsers } from '@/services/user';
import { post } from '@/zzb/utils/request';
import { notification } from 'antd';

export default {
  namespace: 'user',

  state: {
    list: [],
    currentUser: {},
  },

  effects: {
    *fetch(_, { call, put }) {
      const response = yield call(queryUsers);
      yield put({
        type: 'save',
        payload: response,
      });
    },
    *fetchCurrent(_, { call, put }) {
      const response = yield call(post, 'api/Home/GetUserInfo');
      yield put({
        type: 'saveCurrentUser',
        payload: response,
      });
    },
    *changePassword({ data, cancel }, { call }) {
      yield call(post, 'api/Home/ChangePassowrd', data, r => {
        if (r.returnCode === 0) {
          notification.success({ message: '修改成功' });
          cancel();
        }
      });
    },
  },

  reducers: {
    save(state, action) {
      return {
        ...state,
        list: action.payload,
      };
    },
    saveCurrentUser(state, action) {
      return {
        ...state,
        currentUser: action.payload || {},
      };
    },
    changeNotifyCount(state, action) {
      return {
        ...state,
        currentUser: {
          ...state.currentUser,
          notifyCount: action.payload.totalCount,
          unreadCount: action.payload.unreadCount,
        },
      };
    },
  },
};
