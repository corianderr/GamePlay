import { Button } from "@mui/material";
import useAuth from "../../hooks/useAuth";
import { axiosPrivate } from "../../api/axios";
import { useEffect, useState } from "react";

const UserRow = ({user, relation}) => {
    const {auth} = useAuth();
    const [button, setButton] = useState(null);


    useEffect(() => {
        setButton(chooseButton(relation));
    }, [])

    const follow = async () => {

    }

    const accept = async () => {
        console.log(relation);
        const response = await axiosPrivate.post(`User/becomeFriends?id=${user.id}`);
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
    <tr className="candidates-list">
        <td className="title">
            <div className="thumb">
            <img className="img-fluid" src={user.photoPath} alt="avatar" />
            </div>
            <div className="candidate-list-details">
            <div className="candidate-list-info">
                <div className="candidate-list-title">
                <h5 className="mb-0">
                    {user.id === auth?.id && <span>My Profile: </span>}
                    {/* Add link to detailed page */}
                    <a href="#">{user.username}</a>
                </h5>
                </div>
                <div className="candidate-list-option">
                <p>{user.email}</p>
                </div>
            </div>
            </div>
        </td>
        <td id="button-row">
            {relation !== undefined ? button : ''}
        </td>
    </tr>
  )
}

export default UserRow