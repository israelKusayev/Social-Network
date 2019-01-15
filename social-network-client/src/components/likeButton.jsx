import React from 'react';

const LikeButton = (props) => {
  let classes = ' fa fa-heart';
  if (!props.liked) classes += '-o';
  return (
    <>
      <span onClick={props.onClick} style={{ cursor: 'pointer' }}>
        <i className={classes} />
        <span> Likes </span>
      </span>
    </>
  );
};

export default LikeButton;
