import request from '@/utils/request';
import { notification } from 'antd';

const { host } = window;

export async function get(url, data) {
  const res = await request(`${host}/${url}`, {
    credentials: 'include',
    mode: 'cors',
    body: data,
  });
  if (res.returnCode !== 0) {
    notification.error({ message: res.message });
  }
  return res.data;
}

export async function post(url, data, callBack) {
  const res = await request(`${host}/${url}`, {
    credentials: 'include',
    method: 'POST',
    body: data,
    mode: 'cors',
  });
  if (res.returnCode !== 0) {
    notification.error({ message: res.message });
  } else if (callBack) {
    callBack(res);
  }
  return res.data;
}
