import { Post, Get } from './httpService';

const socialUrl = process.env.REACT_APP_SOCIAL_URL + 'comments';

export async function addComment(comment, postId) {
  const json = JSON.stringify({ postId, ...comment });

  return await Post(socialUrl, json, true);
}
export async function getComments(postId) {
  return await Get(`${socialUrl}/${postId}`, true);
}
