import { useEffect, useMemo, useState } from "react";
import { axiosPrivate } from "../../../api/axios";
import { Button } from "@mui/material";
import { useTranslation } from "react-i18next";

const RelationButton = ({ userId, relation, userUpdate }) => {
  const [buttonRelation, setButtonRelation] = useState(null);
  const { t } = useTranslation();

  useEffect(() => {
    setButtonRelation(relation);
  }, [relation, userId]);

  const follow = async () => {
    const response = await axiosPrivate.post(`user/follow?id=${userId}`);
    if (response.data.succeeded) {
      setButtonRelation(1);
      if (userUpdate !== undefined) {
        userUpdate();
      }
    }
  };

  const accept = async () => {
    const response = await axiosPrivate.post(`user/becomeFriends?id=${userId}`);
    if (response.data.succeeded) {
      setButtonRelation(3);
      if (userUpdate !== undefined) {
        userUpdate();
      }
    }
  };

  const chooseButton = (relation) => {
    switch (relation) {
      case 0:
        return (
          <Button
            onClick={() => follow()}
            color="primary"
            variant="contained"
            size="small"
          >
            {t("relationButton.follow")}
          </Button>
        );
      case 1:
        return (
          <Button disabled size="small">
            {t("relationButton.isPending")}
          </Button>
        );
      case 2:
        return (
          <Button
            onClick={() => accept()}
            color="primary"
            variant="contained"
            size="small"
          >
            {t("relationButton.accept")}
          </Button>
        );
      case 3:
        return (
          <Button disabled size="small">
            {t("relationButton.friends")}
          </Button>
        );
      default:
        return <></>;
    }
  };

  return (
    <>
      {
        {
          0: (
            <Button
              onClick={() => follow()}
              color="primary"
              variant="contained"
              size="small"
            >
              {t("relationButton.follow")}
            </Button>
          ),
          1: (
            <Button disabled size="small">
              {t("relationButton.isPending")}
            </Button>
          ),
          2: (
            <Button
              onClick={() => accept()}
              color="primary"
              variant="contained"
              size="small"
            >
              {t("relationButton.accept")}
            </Button>
          ),
          3: (
            <Button disabled size="small">
              {t("relationButton.friends")}
            </Button>
          ),
        }[buttonRelation]
      }
    </>
  );
};

export default RelationButton;
