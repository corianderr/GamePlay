import { useEffect, useMemo, useState } from "react";
import { axiosPrivate } from "../../api/axios";
import { Button } from "@mui/material";

const RelationButton = ({userId, relation, userUpdate}) => {
  const [button, setButton] = useState(null);

  useEffect(() => {
    setButton(chooseButton(relation));
  }, [relation, userId]);

  const follow = async () => {
    const response = await axiosPrivate.post(`User/follow?id=${userId}`);
    if (response.data.succeeded) {
      setButton(chooseButton(1));
      userUpdate();
    }
  };

  const accept = async () => {
    const response = await axiosPrivate.post(`User/becomeFriends?id=${userId}`);
    if (response.data.succeeded) {
      setButton(chooseButton(3));
      userUpdate();
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
            Follow
          </Button>
        );
      case 1:
        return (
          <Button disabled size="small">
            Is Pending...
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
            Accept
          </Button>
        );
      case 3:
        return (
          <Button disabled size="small">
            Friends
          </Button>
        );
      default:
        return <></>;
    }
  };

  return <>{relation !== -1 ? button : ""}</>;
};

export default RelationButton;
