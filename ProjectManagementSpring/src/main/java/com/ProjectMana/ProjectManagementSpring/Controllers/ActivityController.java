package com.ProjectMana.ProjectManagementSpring.Controllers;

import com.ProjectMana.ProjectManagementSpring.DTO.ActivityDTO;
import com.ProjectMana.ProjectManagementSpring.enteties.Task;
import com.ProjectMana.ProjectManagementSpring.enteties.UserActivity;
import com.ProjectMana.ProjectManagementSpring.enteties.UserT;
import com.ProjectMana.ProjectManagementSpring.repo.ActivityRepo;
import jakarta.persistence.criteria.CriteriaBuilder;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.RestController;

import java.time.LocalDateTime;
import java.util.ArrayList;
import java.util.List;

@RestController
public class ActivityController {
  private com.ProjectMana.ProjectManagementSpring.repo.ActivityRepo ActivityRepo ;

  public ActivityController(com.ProjectMana.ProjectManagementSpring.repo.ActivityRepo activityRepo) {
    ActivityRepo = activityRepo;
  }

  @GetMapping("/Activity")
  public List<ActivityDTO> get(){
    List<ActivityDTO> l = new ArrayList<>() ;
    List<UserActivity> l1 = this.ActivityRepo.findAll();
    for(UserActivity u : l1){
      l.add(new ActivityDTO(u.getId(),u.getDate(),u.getStatus(),u.getUser().userName,u.getTask().getTitle()));
    }
    return l ;

  }
  @GetMapping("/Activity/{userId}/{taskId}")
  public List<ActivityDTO> getAct(@PathVariable Integer userId,@PathVariable Integer taskId){
    List<ActivityDTO> l = new ArrayList<>() ;
    List<UserActivity> l1 = this.ActivityRepo.findByUserIdAndTaskId(userId,taskId);
    for(UserActivity u : l1){
      l.add(new ActivityDTO(u.getId(),u.getDate(),u.getStatus(),u.getUser().userName,u.getTask().getTitle()));
    }
    return l ;
  }

}
