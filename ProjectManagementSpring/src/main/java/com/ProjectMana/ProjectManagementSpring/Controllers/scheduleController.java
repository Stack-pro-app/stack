package com.ProjectMana.ProjectManagementSpring.Controllers;

import com.ProjectMana.ProjectManagementSpring.enteties.ScheduleEvent;
import com.ProjectMana.ProjectManagementSpring.repo.scheduleRepo;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RestController;

import java.util.List;

@RestController
public class scheduleController {
private scheduleRepo scheduleRepo  ;

  public scheduleController(com.ProjectMana.ProjectManagementSpring.repo.scheduleRepo scheduleRepo) {
    this.scheduleRepo = scheduleRepo;
  }
  @PostMapping("/schedule/load")
  public List<ScheduleEvent> loadData() {
    return this.scheduleRepo.findAll() ;
  }

  @PostMapping("/schedule/update")

  public List<ScheduleEvent> updateData(@RequestBody EditParams param) {

    if (param.getAction().equals("insert") || (param.getAction().equals("batch") && param.getAdded() != null)) {

      this.scheduleRepo.saveAll(param.getAdded());

    }
    if (param.getAction().equals("update") || (param.getAction().equals("batch") && param.getChanged() != null)) {

      this.scheduleRepo.saveAll(param.getChanged());

    }
    if (param.getAction().equals("remove") || (param.getAction().equals("batch") && param.getDeleted() != null)) {

      for (ScheduleEvent event : param.getDeleted()) {

        this.scheduleRepo.deleteById(event.getId());

      }

    }

    return this.scheduleRepo.findAll();

  }
}
