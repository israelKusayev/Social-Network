import User from '../models/user';

export function convertJsonToUser(jsonUser) {
  const obj = JSON.parse(jsonUser);
  const user = new User();

  for (var prop in obj) {
    user[prop] = obj[prop] ? obj[prop] : '';
  }
  return user;
}
