import React, { useState } from "react";

const StarRating = ({step}) => {
  const [rating, setRating] = useState(0);

  const handleRatingChange = (event) => {
    const newRating = parseFloat(event.target.value);
    console.log(newRating);
    setRating(newRating);

    sendDataToServer(newRating);
  };

  const sendDataToServer = (newRating) => {
    console.log("Sending rating:", newRating);
  };

  return (
    <label className="rating-label">
      <input
        className="rating"
        max="5"
        step={step}
        type="range"
        value={rating}
        onChange={handleRatingChange}
        style={{ '--value': rating }}
      />
    </label>
  );
};

export default StarRating;
