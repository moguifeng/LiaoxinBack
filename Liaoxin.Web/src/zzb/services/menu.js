import { get } from '../utils/request';

export default async function getMenu() {
  return get('api/Home/GetMenu');
}
