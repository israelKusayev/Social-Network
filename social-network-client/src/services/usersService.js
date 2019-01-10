import { Get, Put } from './httpService';

const socialUrl = process.env.REACT_APP_SOCIAL_URL + 'users/';
const identityUrl = process.env.REACT_APP_IDENTITY_URL + 'UsersIdentity/';

export async function getUser(userId) {
  return await Get(identityUrl + userId, true);
}

export async function updateUser(user) {
  return await Put(identityUrl, user, true);
}

export async function getUsers(name) {
  return await Get(socialUrl + 'getUsers/' + name, true);
}
export async function Isfollow(userId) {
  return await Get(socialUrl + 'isFollow/' + userId, true);
}

export async function follow(userId) {
  return await Get(socialUrl + 'follow/' + userId, true);
}

export async function unfollow(userId) {
  return await Get(socialUrl + 'unfollow/' + userId, true);
}

export async function blockUser(blockedUserId) {
  return await Get(socialUrl + 'blockUser/' + blockedUserId, true);
}
export async function unblockUser(blockedUserId) {
  return await Get(socialUrl + 'UnblockUser/' + blockedUserId, true);
}

export async function getFollowings() {
  return await Get(socialUrl + 'following/', true);
}

export async function getFollowers() {
  return await Get(socialUrl + 'followers/', true);
}

export async function getBlockedUsers() {
  return await Get(socialUrl + 'blockedUsers/', true);
}
