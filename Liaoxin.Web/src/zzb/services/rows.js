import { post } from '../utils/request';

export default async function getRowsData(navId, index, size, query) {
  return post('api/Home/GetRowsData', { navId, index, size, query });
}
