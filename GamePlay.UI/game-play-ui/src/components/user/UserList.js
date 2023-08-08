import UserRow from "./UserRow";

const UserList = ({header, users, relations}) => {
    return (
        <>
          <h2 className="text-center">{header}</h2>
          <div className="container mt-3 mb-4">
            <div className="col-lg-9 mt-4 mt-lg-0 mx-auto">
              <div className="row">
                <div className="col-md-12">
                  <div className="user-dashboard-info-box table-responsive mb-0 bg-wheat p-4 shadow-sm">
                    <table className="table manage-candidates-top mb-0">
                      <tbody>
                        {relations !== undefined
                          ? users.map((user, i) => (
                            <UserRow user={user} key={i} relation={relations[i]}/>
                          ))
                          : users.map((user, i) => (
                            <UserRow user={user} key={i}/>
                        ))
                        }
                      </tbody>
                    </table>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </>
      );
}

export default UserList