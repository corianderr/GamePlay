import React, { useEffect, useState } from "react";

const StarRating = ({ step, isChangeable, value }) => {
  const [rating, setRating] = useState(0);

  useEffect(() => {
    if (value !== undefined) {
      setRating(value);
    }
  }, [value]);

  const handleRatingChange = (event) => {
    const newRating = parseFloat(event.target.value);
    setRating(newRating);

    sendDataToServer(newRating);
  };

  const sendDataToServer = (newRating) => {
    console.log("Sending rating:", newRating);
  };

  return (
    <label className="rating-label">
      {isChangeable ? (
        <input
          className="rating"
          max="5"
          step={step}
          type="range"
          value={rating}
          onChange={handleRatingChange}
          style={{ "--value": rating }}
        />
      ) : (
        <input
          className="rating"
          max="5"
          step={step}
          type="range"
          defaultValue={rating}
          style={{ "--value": rating }}
        />
      )}
    </label>
  );
};

export default StarRating;
