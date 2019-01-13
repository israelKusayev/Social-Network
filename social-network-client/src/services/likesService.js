import { Post } from './httpService';

const socialUrl = process.env.REACT_APP_SOCIAL_URL + 'likes';

export async function likePost(postId) {
  return await Post(`${socialUrl}/likePost/${postId}`, null, true);
}

export async function unlikePost(postId) {
  return await Post(`${socialUrl}/UnLikePost/${postId}`, null, true);
}

export async function likeComment(commentId) {
  return await Post(`${socialUrl}/likeComment/${commentId}`, null, true);
}

export async function unlikeComment(commentId) {
  return await Post(`${socialUrl}/unLikeComment/${commentId}`, null, true);
}
