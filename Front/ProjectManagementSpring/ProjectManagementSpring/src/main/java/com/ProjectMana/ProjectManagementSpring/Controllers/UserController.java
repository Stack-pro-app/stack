package com.ProjectMana.ProjectManagementSpring.Controllers;

import com.ProjectMana.ProjectManagementSpring.DTO.userDTO;
import com.ProjectMana.ProjectManagementSpring.enteties.UserT;
import com.ProjectMana.ProjectManagementSpring.repo.UserRepo;
import com.ProjectMana.ProjectManagementSpring.services.userService;
import jakarta.persistence.criteria.CriteriaBuilder;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.List;
import java.util.Set;
import java.util.stream.Collectors;

@RestController
public class UserController {
    private UserRepo userRespo ;
    userService userService;

    public UserController(UserRepo userRespo, com.ProjectMana.ProjectManagementSpring.services.userService userService) {
        this.userRespo = userRespo;
        this.userService = userService;
    }

    @PostMapping("/user")
    public userDTO create(@RequestBody userDTO userDTO){
       return this.userService.post(userDTO);
    }

    @GetMapping ("/user/{id}")
    public List<userDTO> getAll(@PathVariable Integer id){
      List<String> uniqueUserList = h(id);

      List<userDTO> allUsers = this.userService.getAll();


      List<userDTO> filteredUsers = allUsers.stream()
        .filter(user -> uniqueUserList.contains(user.AuthId))
        .collect(Collectors.toList());

      return filteredUsers;

    }

    @GetMapping ("/userAndTasks")
    public List<UserT> getAllTasks(){
        return  this.userRespo.findAll();

    }
  @GetMapping("/user/IdByName/{name}")
  public Integer getIdByName(@PathVariable String name){
    return this.userService.getIdByName(name);

  }
  @GetMapping ("/userTasks")
  public List<UserT> getAllUserTasks(){
    return  this.userRespo.findAll();

  }
  @GetMapping ("/userTasks/{id}")
  public UserT getAllUserTasksId(@PathVariable int id ){
    return  this.userRespo.findById(id).get();

  }
  @GetMapping("/user/sameWork/{pro_id}/{user_id}")
  public ResponseEntity<?> checkSameWork(@PathVariable Integer pro_id, @PathVariable Integer user_id) {
    if (this.userService.sameWork(pro_id, user_id)) {
      return ResponseEntity.ok().build();
    } else {
      return ResponseEntity.status(HttpStatus.INTERNAL_SERVER_ERROR).build();
    }
  }


  public List<String> h(@PathVariable Integer id){
    List<String> users = this.userService.getUsersAdmin(id);


    Set<String> uniqueUsers = users.stream().collect(Collectors.toSet());


    List<String> uniqueUserList = uniqueUsers.stream().collect(Collectors.toList());

    return uniqueUserList;

  }

}
