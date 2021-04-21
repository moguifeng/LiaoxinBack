import { post } from '../utils/request';

export default {
  namespace: 'modal',

  state: {
    fields: [],
    buttons: [],
  },

  effects: {
    *getViewModalInfo({ modalId, data }, { call, put }) {
      const res = yield call(post, 'api/Home/GetModalInfo', { modalId, data });
      yield put({
        type: 'save',
        ...res,
      });
    },

    *handleAction({ buttonId, modalId, data = [], callBack }, { call, put }) {
      const na = {};
      _.each(data, (value, key) => {
        na[key.substring(32)] = value;
      });
      yield call(post, 'api/Home/HandleAction', { modalId, buttonId, data: na }, () => {
        if (callBack) {
          callBack();
        }
      });
      yield put({ type: 'actionButton' });
    },
  },

  reducers: {
    save(state, action) {
      return {
        ...state,
        ...action,
      };
    },

    actionButton(state) {
      return {
        ...state,
      };
    },
  },
};
