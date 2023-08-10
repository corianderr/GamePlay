import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import useAxiosPrivate from "hooks/useAxiosPrivate";
import React, { useEffect, useReducer, useState } from "react";

const StarRating = ({ isEditable, value, game, resetRating }) => {
  const [ratingId, setRatingId] = useState(0);
  const [rating, setRating] = useState(0);
  const [step, setStep] = useState(1);
  const [isChangeable, setIsChangeable] = useState(false);
  const axiosPrivate = useAxiosPrivate();

  useEffect(() => {
    if (value !== undefined) {
      setRating(value.rating);
      setRatingId(value.id);
    }
    setIsChangeable(isEditable);
  }, [value]);

  const handleRatingChange = (event) => {
    const newRating = parseFloat(event.target.value);
    setRating(newRating);
  };

  const deleteRating = (id) => {
    const deleteAsync = async () => {
      const response = await axiosPrivate.delete(`/Game/deleteRating/${id}`);
      console.log(response);
      if (response.data.succeeded) {
        setIsChangeable(true);
        setStep(1);
        resetRating();
      }
    };
    deleteAsync();
  };

  const addRating = (gameId, rating) => {
    const addAsync = async () => {
      const response = await axiosPrivate.post(
        `/Game/rateGame?id=${gameId}&rating=${rating}`
      );
      console.log(response);
      if (response.data.succeeded) {
        setIsChangeable(false);
        setStep(0.1);
        setRatingId(response.data.result.id);
        resetRating();
      }
    };
    addAsync();
  };

  return (
    <>
      <div className="d-flex">
        {isChangeable ? (
          <>
            <label className="rating-label">
              <input
                className="rating"
                max="5"
                step={step}
                type="range"
                value={rating}
                onChange={handleRatingChange}
                style={{ "--value": rating }}
              />
            </label>
            <button
              icon="fa-solid fa-trash"
              onClick={() => addRating(game.id, rating)}
              className="my-auto btn btn-secondary btn-sm save-button"
            >
              Save
            </button>
          </>
        ) : (
          <>
            <label className="rating-label">
              <input
                className="rating"
                max="5"
                step={step}
                type="range"
                defaultValue={rating}
                style={{ "--value": rating }}
              />
            </label>

            <span className="my-auto">
              {rating}
              <FontAwesomeIcon
                icon="fa-solid fa-trash"
                style={{ color: "#696e77", cursor: "pointer" }}
                onClick={() => deleteRating(ratingId)} className="ms-2"
              />
            </span>
          </>
        )}
      </div>
      {game.averageRating !== undefined && (  
        <span className="my-auto">
          Average Rating: {game.averageRating.toFixed(2)}
        </span>
      )}
    </>
  );
};

export default StarRating;
