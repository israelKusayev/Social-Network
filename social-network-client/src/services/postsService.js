import { Post } from './httpService';

const socialUrl = process.env.REACT_APP_SOCIAL_URL + 'post';
export async function createPost(post) {
  const json = JSON.stringify(post);
  return await Post(socialUrl, json, true);
}
