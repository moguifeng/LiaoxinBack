import { routerRedux } from 'dva/router';
import { getFakeCaptcha } from '@/services/api';
import { setAuthority } from '@/utils/authority';
import { getPageQuery } from '@/utils/utils';
import { reloadAuthorized } from '@/utils/Authorized';
import { post } from '@/zzb/utils/request';

export default {
  namespace: 'login',

  state: {
    status: undefined,
  },

  effects: {
    *login({ payload }, { call, put }) {
      const res = yield call(post, 'api/Home/login', {
        name: payload.userName,
        password: payload.password,
      });

      if (res && String(res).length !== 0) {
        const response = {
          currentAuthority: 'admin',
          status: 'ok',
          type: 'account',
        };
        yield put({
          type: 'changeLoginStatus',
          payload: response,
        });
        // Login successfully

        if (response.status === 'ok') {
          reloadAuthorized();
          const urlParams = new URL(window.location.href);

          const storage = window.localStorage;
          const tokenStr = 'Token';
          storage.setItem(tokenStr, res);

          const params = getPageQuery();
          let { redirect } = params;

          if (redirect) {
            const redirectUrlParams = new URL(redirect);
            if (redirectUrlParams.origin === urlParams.origin) {
              redirect = redirect.substr(urlParams.origin.length);
              if (redirect.match(/^\/.*#/)) {
                redirect = redirect.substr(redirect.indexOf('#') + 1);
              }
            } else {
              window.location.href = redirect;
              return;
            }
          } else {
            redirect = '/';
          }
          window.location.href = redirect;
        }
      }
    },

    *getCaptcha({ payload }, { call }) {
      yield call(getFakeCaptcha, payload);
    },

    *logout(_, { put }) {
      yield put({
        type: 'changeLoginStatus',
        payload: {
          status: false,
          currentAuthority: 'guest',
        },
      });
      reloadAuthorized();
      yield put(
        routerRedux.push({
          pathname: '/user/login',
        })
      );
    },
  },

  reducers: {
    changeLoginStatus(state, { payload }) {
      setAuthority(payload.currentAuthority);
      return {
        ...state,
        status: payload.status,
        type: payload.type,
      };
    },
  },
};
