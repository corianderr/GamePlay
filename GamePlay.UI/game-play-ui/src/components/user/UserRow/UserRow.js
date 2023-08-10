import useAuth from "../../../hooks/useAuth";
import RelationButton from "../RelationButton/RelationButton";
import { Link } from "react-router-dom";

const UserRow = ({ user, relation }) => {
  const { auth } = useAuth();

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
                    <Link to={`/userDetails/${user.id}`}>{user.userName}</Link>
                </h5>
                </div>
                <div className="candidate-list-option">
                <p>{user.email}</p>
                </div>
            </div>
            </div>
        </td>
        <td id="button-row">
            <RelationButton relation={relation} userId={user.id}/>
        </td>
    </tr>
  );
};

export default UserRow;
