import { post } from '@/zzb/utils/request';
import { notification } from 'antd';

export default {
  namespace: 'systemConfig',

  state: { keys: [], images: [] },

  effects: {
    *getSystemConfigSettings(_, { call, put }) {
      const res = yield call(post, 'api/SystemConfig/GetSystemConfigSettings');
      yield put({
        type: 'save',
        keys: res,
      });
    },
    *saveConfigs({ data }, { call }) {
      yield call(post, 'api/SystemConfig/Save', data, () => {
        notification.success({ message: '保存成功' });
      });
    },
    *getImageSetting(_, { call, put }) {
      const res = yield call(post, 'api/SystemConfig/GetImageSetting');
      yield put({
        type: 'save',
        images: res,
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
