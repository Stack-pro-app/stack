using AutoMapper;
using messaging_service.Controllers;
using messaging_service.Repository.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace messaging_service.tests.controller.tests
{
    public class WorkspaceTests
    {
        public IWorkspaceRepository workspaceRepository { get; set; }
        public IMapper mapper { get; set; }
        public WorkspaceController Controller { get; set; }
        public WorkspaceTests()
        {
            workspaceRepository = Mock.Of<IWorkspaceRepository>();
            mapper = Mock.Of<IMapper>();
            Controller = new WorkspaceController(workspaceRepository, mapper);
        }

    }
}
