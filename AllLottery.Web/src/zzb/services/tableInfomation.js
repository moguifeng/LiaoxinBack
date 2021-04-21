import { post } from '../utils/request';

export default async function getColumns(navId) {
  return post('api/Home/GetTableInfomation', { navId });
}
