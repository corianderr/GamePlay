import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { Rating } from "@mui/material";
import useAuth from "hooks/useAuth";
import useAxiosPrivate from "hooks/useAxiosPrivate";
import React, { useEffect, useReducer, useState } from "react";
import { useTranslation } from "react-i18next";
import { toast } from "react-toastify";

const StarRating = ({ isEditable, value, game, resetRating }) => {
  const { auth } = useAuth();
  const [ratingId, setRatingId] = useState(0);
  const [rating, setRating] = useState(0);
  const [step, setStep] = useState(1);
  const [isChangeable, setIsChangeable] = useState(false);
  const axiosPrivate = useAxiosPrivate();
  const { t } = useTranslation();

  useEffect(() => {
    if (value?.rating !== undefined) {
      setRating(value.rating);
      setRatingId(value.id);
    }
    setIsChangeable(isEditable);
  }, [value]);

  useEffect(() => {
    if (game?.averageRating !== undefined && rating === 0) {
      setRating(parseFloat(game.averageRating.toFixed(2)));
    }
  }, [game]);

  const deleteRating = () => {
    const deleteAsync = async () => {
      const response = await axiosPrivate.delete(
        `/game/deleteRating/${ratingId}`
      );
      if (response.data.succeeded) {
        setIsChangeable(true);
        setStep(1);
        resetRating();
      }
    };
    deleteAsync();
  };

  const addRating = (gameId) => {
    if (rating === undefined) {
      toast.error(t("game.rateValidationMes"));
      return;
    }

    const addAsync = async () => {
      const response = await axiosPrivate.post(
        `/Game/rateGame?id=${gameId}&rating=${rating}`
      );
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
      {auth?.id === undefined ? (
        <div>
          <Rating
            name="simple-controlled"
            precision={0.1}
            value={rating}
            size="large"
            readOnly
            className="me-2"
          />
        </div>
      ) : (
        <div className="d-flex">
          {isChangeable ? (
            <>
              <Rating
                name="simple-controlled"
                precision={step}
                value={rating}
                size="large"
                className="me-2"
                onChange={(event, newValue) => {
                  setRating(newValue);
                }}
              />
              <button
                icon="fa-solid fa-trash"
                onClick={() => addRating(game.id)}
                className="my-auto btn btn-secondary btn-sm save-button"
              >
                {t("forms.save")}
              </button>
            </>
          ) : (
            <>
              <Rating
                name="simple-controlled"
                precision={step}
                value={rating}
                size="large"
                className="me-2"
                onChange={(event, newValue) => {
                  setRating(newValue);
                }}
                readOnly
              />

              <span className="my-auto">
                {rating}
                <FontAwesomeIcon
                  icon="fa-solid fa-trash"
                  style={{ color: "#696e77", cursor: "pointer" }}
                  onClick={deleteRating}
                  className="ms-2"
                />
              </span>
            </>
          )}
        </div>
      )}
      {game.averageRating !== undefined && (
        <span className="my-auto">
          {t("game.averageRating")}: {game.averageRating.toFixed(2)}
        </span>
      )}
    </>
  );
};

export default StarRating;
