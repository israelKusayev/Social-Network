import React from 'react';

const TextPink = (WrappedComponent) => {
  return (
    <div className="text-pink">
      <WrappedComponent {...this.props} />
    </div>
  );
};
export default TextPink;
