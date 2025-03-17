using Xunit;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BTLNhapMonCNPM.Controllers;
using BTLNhapMonCNPM.Models;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace BTLNhapMonCNPM.BTLNhapMonCNPM.Tests
{
	public class SearchProductControllerTests
	{
		private readonly Mock<PharmacyDbContext> _mockContext;
		private readonly SearchProductController _controller;

		public SearchProductControllerTests()
		{
			_mockContext = new Mock<PharmacyDbContext>();
			_controller = new SearchProductController(_mockContext.Object);
		}

		[Fact]
		public async Task Search_ReturnsJsonResult_WithProducts()
		{
			// Arrange
			var keyword = "Product";
			var sanPhamList = new List<TblSanPham>
			{
				new TblSanPham { IMaSp = 1, STen = "Product1", FGiaBan = 100, ISoLuong = 10, SImageThuoc = "/images/product1.png", IMaNcc = 1 },
				new TblSanPham { IMaSp = 2, STen = "Product2", FGiaBan = 200, ISoLuong = 20, SImageThuoc = "/images/product2.png", IMaNcc = 2 }
			};
			var nhaCungCapList = new List<TblNhaCungCap>
			{
				new TblNhaCungCap { IMaNcc = 1, STenNcc = "Supplier1" },
				new TblNhaCungCap { IMaNcc = 2, STenNcc = "Supplier2" }
			};

			var sanPhamDbSet = GetQueryableMockDbSet(sanPhamList);
			var nhaCungCapDbSet = GetQueryableMockDbSet(nhaCungCapList);

			_mockContext.Setup(c => c.TblSanPhams).Returns(sanPhamDbSet.Object);
			_mockContext.Setup(c => c.TblNhaCungCaps).Returns(nhaCungCapDbSet.Object);

			// Act
			var result = await _controller.Search(keyword);

			// Assert
			var jsonResult = Assert.IsType<JsonResult>(result);
			var data = Assert.IsAssignableFrom<IEnumerable<object>>(jsonResult.Value);
			Assert.NotEmpty(data);
		}

		[Fact]
		public async Task Search_ReturnsJsonResult_WithNoDataMessage_WhenKeywordIsEmpty()
		{
			// Arrange
			var keyword = "";

			// Act
			var result = await _controller.Search(keyword);

			// Assert
			var jsonResult = Assert.IsType<JsonResult>(result);
			var data = jsonResult.Value;
			Assert.Equal(new { success = false, message = "Không có dữ liệu." }, data);
		}

		private Mock<DbSet<T>> GetQueryableMockDbSet<T>(List<T> sourceList) where T : class
		{
			var queryable = sourceList.AsQueryable();
			var dbSet = new Mock<DbSet<T>>();
			dbSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(new TestAsyncQueryProvider<T>(queryable.Provider));
			dbSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
			dbSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
			dbSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(queryable.GetEnumerator());
			dbSet.As<IAsyncEnumerable<T>>().Setup(m => m.GetAsyncEnumerator(default)).Returns(new TestAsyncEnumerator<T>(queryable.GetEnumerator()));
			return dbSet;
		}

		private class TestAsyncQueryProvider<TEntity> : IAsyncQueryProvider
		{
			private readonly IQueryProvider _inner;

			internal TestAsyncQueryProvider(IQueryProvider inner)
			{
				_inner = inner;
			}

			public IQueryable CreateQuery(Expression expression)
			{
				return new TestAsyncEnumerable<TEntity>(expression);
			}

			public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
			{
				return new TestAsyncEnumerable<TElement>(expression);
			}

			public object Execute(Expression expression)
			{
				return _inner.Execute(expression);
			}

			public TResult Execute<TResult>(Expression expression)
			{
				return _inner.Execute<TResult>(expression);
			}

			public IAsyncEnumerable<TResult> ExecuteAsync<TResult>(Expression expression)
			{
				return new TestAsyncEnumerable<TResult>(expression);
			}

			public TResult ExecuteAsync<TResult>(Expression expression, CancellationToken cancellationToken)
			{
				return Execute<TResult>(expression);
			}
		}

		private class TestAsyncEnumerable<T> : EnumerableQuery<T>, IAsyncEnumerable<T>, IQueryable<T>
		{
			public TestAsyncEnumerable(IEnumerable<T> enumerable) : base(enumerable)
			{ }

			public TestAsyncEnumerable(Expression expression) : base(expression)
			{ }

			public IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default)
			{
				return new TestAsyncEnumerator<T>(this.AsEnumerable().GetEnumerator());
			}

			IQueryProvider IQueryable.Provider
			{
				get { return new TestAsyncQueryProvider<T>(this); }
			}
		}

		private class TestAsyncEnumerator<T> : IAsyncEnumerator<T>
		{
			private readonly IEnumerator<T> _inner;

			public TestAsyncEnumerator(IEnumerator<T> inner)
			{
				_inner = inner;
			}

			public ValueTask DisposeAsync()
			{
				_inner.Dispose();
				return ValueTask.CompletedTask;
			}

			public ValueTask<bool> MoveNextAsync()
			{
				return new ValueTask<bool>(_inner.MoveNext());
			}

			public T Current
			{
				get { return _inner.Current; }
			}
		}
	}
}
