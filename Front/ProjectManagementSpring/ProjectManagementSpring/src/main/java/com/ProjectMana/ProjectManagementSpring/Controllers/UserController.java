package com.ProjectMana.ProjectManagementSpring.Controllers;

import com.ProjectMana.ProjectManagementSpring.DTO.userDTO;
import com.ProjectMana.ProjectManagementSpring.enteties.UserT;
import com.ProjectMana.ProjectManagementSpring.repo.UserRepo;
import com.ProjectMana.ProjectManagementSpring.services.userService;
import org.springframework.web.bind.annotation.*;

import java.util.List;

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

    @GetMapping ("/user")
    public List<userDTO> getAll(){
        return  this.userService.getAll();

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
}
