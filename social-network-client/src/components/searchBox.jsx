import React from 'react';

const SearchBox = ({ value, onChange, onBlur }) => {
  return (
    <input
      type="text"
      id="query"
      className="form-control  mr-sm-2"
      placeholder="Search..."
      value={value}
      autoComplete="off"
      onChange={(e) => onChange(e.currentTarget.value)}
      onBlur={onBlur}
    />
  );
};

export default SearchBox;
