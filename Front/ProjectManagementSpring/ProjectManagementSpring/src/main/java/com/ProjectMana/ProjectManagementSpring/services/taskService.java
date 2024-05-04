package com.ProjectMana.ProjectManagementSpring.services;

import com.ProjectMana.ProjectManagementSpring.DTO.*;
import com.ProjectMana.ProjectManagementSpring.enteties.Task;
import com.ProjectMana.ProjectManagementSpring.enteties.UserT;
import com.ProjectMana.ProjectManagementSpring.enteties.project;
import com.ProjectMana.ProjectManagementSpring.repo.TaskRepo;
import com.ProjectMana.ProjectManagementSpring.repo.projectRepo;
import jakarta.persistence.criteria.CriteriaBuilder;
import org.springframework.stereotype.Service;



import java.time.Duration;
import java.time.LocalDate;
import java.time.format.DateTimeFormatter;
import java.util.ArrayList;
import java.util.List;
import java.time.temporal.TemporalUnit;

@Service
public class taskService {
    private TaskRepo taskRepo ;
    private projectRepo  projectRepo ;
    private projectService projectService ;

  public taskService(TaskRepo taskRepo, com.ProjectMana.ProjectManagementSpring.repo.projectRepo projectRepo, com.ProjectMana.ProjectManagementSpring.services.projectService projectService) {
    this.taskRepo = taskRepo;
    this.projectRepo = projectRepo;
    this.projectService = projectService;
  }

  public taskDTO create(taskDTO t ){

        Task ts = new Task(t.getNo(),t.title,t.getDescription(),t.getEnd(),t.start,t.getStatus(),new project(t.projectId),new UserT(t.userId));
        Task ts1 = this.taskRepo.save(ts);
        t.no=ts1.getNo();
        return t ;

    }
    public List<taskDTO1> getAll(){
        List<taskDTO1> l = new ArrayList<>();
        List<Task> l1 = this.taskRepo.findAll();
        if(!l1.isEmpty()){
            for(Task t : l1){
                l.add(new taskDTO1(t.getNo(),t.getTitle(),t.getDescription(),t.getProject().getProjectName(),t.getUser().userName,t.getEnd(),t.getStatus(),t.getStart()));
            }

        }
        return l ;

    }
    public taskDTO1 getByNo(Integer no ){
        Task t  = this.taskRepo.findById(no).get();

        return new taskDTO1(t.getNo(),t.getTitle(),t.getDescription(),t.getProject().getProjectName(),t.getUser().userName,t.getEnd(),t.getStatus(),t.getStart());

    }
  /*  public List<GanttDTO> getGantData(){
    List<GanttDTO> l = new ArrayList<>();
      List<project> l1 = this.projectRepo.findAll();
      for (project p : l1 ){

        GanttDTO g = new GanttDTO(p.getId(),p.getProjectName(),p.getStart(),p.getEnd());
        g.tasks=new ArrayList<>();
        for(Task t : p.getTasks()){

          DateTimeFormatter formatter = DateTimeFormatter.ofPattern("yyyy-MM-dd");

          LocalDate date1 = LocalDate.parse(t.getStart(), formatter);
          LocalDate date2 = LocalDate.parse(t.getEnd(), formatter);
          long daysBetween = Duration.between(date1, date2).toHours();
         // long daysBetween = Duration.between(date1, date2).toDays();


          TaskGant tg = new TaskGant(t.getNo(),t.getTitle(),t.getStart(), daysBetween,t.getStatus(),t.getUser().userName);
          g.tasks.add(tg);


        }
        l.add(g);

      }
      return l ;

    }*/
  public List<project> pp(){
   return this.projectRepo.findAll();
  }
  public Task updateProgress(Integer no ,int progress ){

    Task t =  this.taskRepo.findById(no).get();
    t.setStatus(progress);
    return this.taskRepo.save(t);


  }


  public List<taskDTO1> getAll0(Integer id) {
    List<taskDTO1> l = new ArrayList<>();
    List<project> projects  =  this.projectService.getAdminProject0(id) ;
    List<Task> l1 = this.taskRepo.findAllByProjectIn(projects);
    if(!l1.isEmpty()){
      for(Task t : l1){
        l.add(new taskDTO1(t.getNo(),t.getTitle(),t.getDescription(),t.getProject().getProjectName(),t.getUser().userName,t.getEnd(),t.getStatus(),t.getStart()));
      }

    }
    return l ;

  }
  public List<Task> getAllTasksAdmin1(Integer id) {

    List<project> projects  =  this.projectService.getAdminProject0(id) ;
    List<Task> l1 = this.taskRepo.findAllByProjectIn(projects);
   return l1 ;

  }
}
