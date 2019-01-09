import { Get, Put } from './httpService';

const socialUrl = process.env.REACT_APP_SOCIAL_URL + 'users/';
const identityUrl = process.env.REACT_APP_IDENTITY_URL + 'UsersIdentity/';

export async function getUser(userId) {
  return await Get(identityUrl + userId, true);
}

export async function getUsers(name) {
  return await Get(socialUrl + 'getUsers/' + name, true);
}

export async function updateUser(user) {
  return await Put(identityUrl, user, true);
}
