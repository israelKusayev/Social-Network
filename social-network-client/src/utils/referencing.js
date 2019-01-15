import React from 'react';
import { Link } from 'react-router-dom';

export const onReferenceSelect = (user, content) => {
  const index = content.lastIndexOf('@');
  content = content.slice(0, index + 1);
  content += user.UserName;
  return { content, reference: { ...user, startIndex: index, endIndex: index + user.UserName.length } };
};

export const convertContent = (content, refs) => {
  if (refs.length !== 0) {
    refs = refs.sort((a, b) => a.StartIndex - b.StartIndex);
    let newContent = [];
    for (let i = 0; i < refs.length; i++) {
      newContent.push(
        <span key={refs[i].UserId + i}>
          <span>
            {i === 0
              ? content.slice(0, refs[i].StartIndex)
              : content.slice(refs[i - 1].EndIndex + 1, refs[i].StartIndex)}
          </span>
          <Link className="text-pink" to={'/profile/' + refs[i].UserId}>
            {content.slice(refs[i].StartIndex, refs[i].EndIndex + 1)}
          </Link>
          {i === refs.length - 1 ? <span>{content.slice(refs[refs.length - 1].EndIndex + 1)}</span> : null}
        </span>
      );
    }
    return newContent;
  } else {
    return content;
  }
};
