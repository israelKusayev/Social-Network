import React from 'react';

const ListGroup = ({ items, labelProperty, textProperty, keyProperty, selectedItem, onItemSelect }) => {
  return (
    <ul className="list-group ">
      {items.map((item) => (
        <li
          onClick={() => onItemSelect(item)}
          key={item[keyProperty]}
          className={
            item[textProperty] === selectedItem
              ? 'list-group-item list-group-item-dark selected'
              : 'list-group-item list-group-item-dark'
          }
        >
          {item[labelProperty] || item[textProperty]}
        </li>
      ))}
    </ul>
  );
};

ListGroup.defaultProps = {
  labelProperty: 'label',
  textProperty: 'name',
  keyProperty: 'id'
};

export default ListGroup;
