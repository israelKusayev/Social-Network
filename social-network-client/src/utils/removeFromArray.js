export const removeItemFromArray = (array, item) => {
  const index = array.indexOf(item);
  array.splice(index, 1);
  return array;
};
