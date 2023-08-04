import useAuth from "../../hooks/useAuth";

const UserRow = ({user}) => {
    const {auth} = useAuth();

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
    </tr>
  )
}

export default UserRow