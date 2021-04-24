import { notification } from 'antd';
import request from '@/utils/request';

const { host } = window;


const  storage  =window.localStorage;
  const tokenStr = "Token"

export async function get(url, data) {


  const res = await request(`${host}/${url}`, {
    credentials: 'include',
    mode: 'cors',
    body: data,
    headers:{"Token": storage.getItem(tokenStr)}

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
    headers:{"Token": storage.getItem(tokenStr)}
  });
  if (res.returnCode !== 0) {
    notification.error({ message: res.message });
  } else if (callBack) {
    callBack(res);
  }
  return res.data;
}
