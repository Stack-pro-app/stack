package com.ProjectMana.ProjectManagementSpring.Controllers;

import com.ProjectMana.ProjectManagementSpring.DTO.GanttDTO;
import com.ProjectMana.ProjectManagementSpring.DTO.taskDTO;
import com.ProjectMana.ProjectManagementSpring.DTO.taskDTO1;
import com.ProjectMana.ProjectManagementSpring.enteties.Task;
import com.ProjectMana.ProjectManagementSpring.enteties.UserActivity;
import com.ProjectMana.ProjectManagementSpring.enteties.UserT;
import com.ProjectMana.ProjectManagementSpring.enteties.project;
import com.ProjectMana.ProjectManagementSpring.repo.TaskRepo;
import com.ProjectMana.ProjectManagementSpring.services.taskService;
import org.springframework.web.bind.annotation.*;
import com.ProjectMana.ProjectManagementSpring.repo.ActivityRepo ;

import java.time.LocalDateTime;
import java.util.List;

@RestController
public class TaskController {
    private TaskRepo TaskRepo ;

  private taskService taskService;
  private ActivityRepo ActivityRepo ;



  public TaskController(com.ProjectMana.ProjectManagementSpring.repo.TaskRepo taskRepo, com.ProjectMana.ProjectManagementSpring.services.taskService taskService, ActivityRepo activityRepo) {
    TaskRepo = taskRepo;
    this.taskService = taskService;
    ActivityRepo = activityRepo;
  }

  @PostMapping("/task")
    public taskDTO create(@RequestBody taskDTO Task){
      return this.taskService.create(Task);

    }

    @GetMapping("/task")
    public List<taskDTO1> getAll(){
        return this.taskService.getAll();
    }
  @GetMapping("/taskAdmin/{id}")
  public List<taskDTO1> getAll0(@PathVariable Integer id ){
    return this.taskService.getAll0(id);
  }

    @DeleteMapping("/task/{id}")
    public void delete(@PathVariable Integer id ){
      Task task = this.TaskRepo.findById(id)
        .orElseThrow(() -> new RuntimeException("Task not found with id: " + id));

      this.ActivityRepo.deleteByTask(task);

        this.TaskRepo.deleteById(id);

    }

    @GetMapping("/task/{no}")
    public taskDTO1 getByNo(@PathVariable Integer no){
        return this.taskService.getByNo(no);
    }
  @GetMapping("/task/Gantt")
  public List<project> getGantData(){
      return this.taskService.pp();
  }
  @GetMapping("/task/{no}/{progress}")
  public Task updateProgress(@PathVariable int no , @PathVariable int progress){

      Task t  = this.TaskRepo.findById(no).get();
      Integer userId = t.getUser().id ;
      UserActivity u = new UserActivity() ;
      u.setDate(LocalDateTime.now());
      u.setUser(new UserT(userId));
      u.setTask(new Task(t.getNo()));
      u.setStatus(progress);
      this.ActivityRepo.save(u);

    return this.taskService.updateProgress(no,progress);

  }





}
