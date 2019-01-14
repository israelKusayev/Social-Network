export const onReferenceSelect = (user, content) => {
  const index = content.lastIndexOf('@');
  content = content.slice(0, index + 1);
  content += user.UserName;
  return { content, reference: { ...user, startIndex: index, endIndex: index + user.UserName.length } };
};
