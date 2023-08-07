import { useEffect, useState } from "react";
import { axiosPrivate } from "../../api/axios";
import { Button } from "@mui/material";

const RelationButton = (userId, relation) => {
    const [button, setButton] = useState(null);

    useEffect(() => {
        setButton(chooseButton(relation));
    }, [])

    const follow = async () => {
        console.log(relation);
        const response = await axiosPrivate.post(`User/follow?id=${userId}`);
        console.log(response);
        if(response.data.succeeded) 
        {
            setButton(chooseButton('pending'));
        }
    }

    const accept = async () => {
        console.log(relation);
        const response = await axiosPrivate.post(`User/becomeFriends?id=${userId}`);
        console.log(response);
        if(response.data.succeeded) 
        {
            setButton(chooseButton('friends'));
        }
    }

    const chooseButton = (relation) => {
        console.log(relation)
        switch(relation) {
          case "doesNotExist":
            return <Button onClick={() => follow()} color="primary" variant="contained" size="small">Follow</Button>;
          case "pending":
            return <Button disabled size="small">Is Pending...</Button>;
          case "friends":
            return <Button disabled size="small">Friends</Button>;
          case "accept":
            return <Button onClick={() => accept()} color="primary" variant="contained" size="small">Accept</Button>;
          default:
            return <></>;
        }
    };

  return (
    <>{relation !== undefined ? button : ''}</>
  )
}

export default RelationButton