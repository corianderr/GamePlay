import { useEffect, useState } from "react";
import { axiosPrivate } from "../../api/axios";
import { Button } from "@mui/material";

const RelationButton = ({userId, relation}) => {
  const [button, setButton] = useState(null);

  useEffect(() => {
    console.log("RELATIONBUTTON");
    console.log(relation);
    console.log(userId);
    setButton(chooseButton(relation));
  }, []);

  const follow = async () => {
    console.log(relation);
    const response = await axiosPrivate.post(`User/follow?id=${userId}`);
    console.log(response);
    if (response.data.succeeded) {
      setButton(chooseButton(1));
    }
  };

  const accept = async () => {
    console.log(relation);
    const response = await axiosPrivate.post(`User/becomeFriends?id=${userId}`);
    console.log(response);
    if (response.data.succeeded) {
      setButton(chooseButton(3));
    }
  };

  const chooseButton = (relation) => {
    console.log(relation);
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
